using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EmbSysRegView
{
    public class OpenOcdTclClient
    {
        public enum TargetEventType
        {
            GdbHalt,
            Halted,
            Resumed,
            ResumeStart,
            ResumeEnd,
            GdbStart,
            GdbEnd,
            ResetStart,
            ResetAssertPre,
            ResetAssert,
            ResetAssertPost,
            ResetDeassertPre,
            ResetDeassertPost,
            ResetHaltPre,
            ResetHaltPost,
            ResetWaitPre,
            ResetWaitPost,
            ResetInit,
            ResetEnd,
            DebugHalted,
            DebugResumed,
            ExamineStart,
            ExamineEnd,
            GdbAttach,
            GdbDetach,
            GdbFlashEraseStart,
            GdbFlashEraseEnd,
            GdbFlashWriteStart,
            GdbFlashWriteEnd,
            Unknown
        }

        #region Properties

        public readonly string Hostname;
        public readonly int Port;
        public bool Connected { get; private set; }

        #endregion

        #region Commands

        public uint ReadMemory(uint address)
        {
            uint ret = 0;

            var result = DoCommand(String.Format("ocd_mdw 0x{0:X}", address));
            if (result == null) return ret;

            var addr = result.Split(':')[0].Trim();
            var val = result.Split(':')[1].Trim();
            ret = Convert.ToUInt32(val, 16);

            return ret;
        }

        #endregion

        #region Public Functions

        public OpenOcdTclClient(Control control, string hostname = "localhost", int port = 6666)
        {
            Hostname = hostname;
            Port = port;
            Connected = false;
            this.control = control;

            thread = new Thread(this.ThreadMain);
            thread.IsBackground = true;
        }

        public void Start()
        {
            if (!started)
            {
                thread.Start();
                started = true;
            }
        }

        #endregion

        #region Events

        public class TargetEventArgs : EventArgs
        {
            public TargetEventType EventType;

            public TargetEventArgs(TargetEventType e)
            {
                EventType = e;
            }
        }

        public delegate void TargetEventHandler(object sender, TargetEventArgs args);
        public TargetEventHandler TargetEvent;
        public void OnTargetEvent(TargetEventType e)
        {
            if (TargetEvent != null)
            {
                Invoke(() => TargetEvent(this, new TargetEventArgs(e)));
            }
        }

        public EventHandler ConnectionChanged;
        public void OnConnectionChanged()
        {
            if (ConnectionChanged != null)
            {
                Invoke(() => ConnectionChanged(this, new EventArgs()));
            }
        }

        #endregion

        #region Internal Implementation

        private class Command
        {
            public readonly string Send;
            public string Result;
            public readonly ManualResetEvent Completed;

            public Command(string command)
            {
                Send = command;
                Completed = new ManualResetEvent(false);
            }
        }

        private Thread thread;
        private Socket socket = null;
        private Control control;
        private ConcurrentQueue<Command> commands = new ConcurrentQueue<Command>();
        private bool started = false;

        private void ThreadMain()
        {
            while (true)
            {
                // dispose non connected sockets
                if (socket != null && (!socket.Connected || Connected == false))
                {
                    socket.Dispose();
                    socket = null;
                    Connected = false;
                    OnConnectionChanged();
                }

                // try reconnection
                if (socket == null)
                {
                    socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        // connect to server
                        socket.Connect(Hostname, Port);

                        // turn on event notifications
                        SendCommand("tcl_events on");

                        Connected = true;
                        OnConnectionChanged();
                    }
                    catch (SocketException)
                    {
                        // connection failed, delay retry
                        Thread.Sleep(100);
                        continue;
                    }
                }

                try
                {
                    // main connection loop
                    while (true)
                    {
                        Command command;

                        // check connected
                        if ((socket.Poll(1000, SelectMode.SelectRead) && (socket.Available == 0)) || !socket.Connected)
                        {
                            Connected = false;
                            break;
                        }

                        // handle commands
                        while (commands.TryDequeue(out command))
                        {
                            command.Result = SendCommand(command.Send);
                            command.Completed.Set();
                        }

                        // handle poll
                        var eventText = ReceiveResponse(true);
                        if (eventText == null) continue;

                        // handle events
                        HandleEvent(eventText);
                    }
                }
                catch (SocketException)
                {
                    // socket failed, next loop will dispose and reconnect
                    Connected = false;
                    continue;
                }
            }
        }

        private bool HandleEvent(string response)
        {
            if (!response.StartsWith("#EVENT 0x")) return false;

            var str = response.Substring(response.IndexOf("0x"));
            var eventVal = -1;
            try
            {
                eventVal = Convert.ToInt32(str, 16);
            }
            catch { }
            if (eventVal < 0 || eventVal > (int)TargetEventType.Unknown)
                return true;

            OnTargetEvent((TargetEventType)eventVal);

            return true;
        }

        private string SendCommand(string command)
        {
            // encode the command string and add the terminator
            var buffer = Encoding.UTF8.GetBytes(command + "\x1a");

            // send the buffer
            socket.Send(buffer);

            // receive the response, and handle timing issues with events
            string resp;
            do
            {
                resp = ReceiveResponse();
            } while (HandleEvent(resp));

            return resp;
        }

        private string DoCommand(string command, int timeout = 1000)
        {
            var cmd = new Command(command);
            commands.Enqueue(cmd);
            cmd.Completed.WaitOne();
            return cmd.Result;
        }

        private string ReceiveResponse(bool onlyIfAvailable = false)
        {
            var buffer = new byte[4096];
            int offset = 0;

            // bail if we aren't supposed to block and nothing is available
            if (onlyIfAvailable && socket.Available == 0) return null;

            // read until the buffer is full or we have a terminator
            while (offset < buffer.Length)
            {
                offset += socket.Receive(buffer, offset, buffer.Length - offset, SocketFlags.None);
                if (buffer.Contains((byte)0x1a)) break;
            }

            // get the index of the terminator
            var end = Array.IndexOf<byte>(buffer, 0x1a);

            // decode the bytes
            return Encoding.UTF8.GetString(buffer, 0, end).Trim();
        }

        private void Invoke(Action action)
        {
            control.BeginInvoke((Delegate)action);
        }

        #endregion
    }
}
