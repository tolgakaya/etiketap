using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EtikeTAP
{
    public class WaitIndicator : IDisposable
    {
        ProgressForm progressForm;
        Thread thread;
        bool disposed = false; //to avoid redundant call
        public WaitIndicator()
        {
            progressForm = new ProgressForm();
            thread = new Thread(_ => progressForm.ShowDialog());
            thread.Start();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                thread.Abort();
                progressForm = null;
            }
            disposed = true;
        }
    }
}
