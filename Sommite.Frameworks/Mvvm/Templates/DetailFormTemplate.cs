using DevExpress.XtraEditors;
using Sommite.Frameworks.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sommite.Frameworks.MVVM
{
    public abstract class DetailFormTemplate<T>:XtraForm
    {

        public abstract Type GetType();

        public event EventHandler Changed;

        public void OnChanged(object sender, EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        public abstract void InitializeControlsAndHandlers();

        public abstract BindingProvider<T> BindingProvider();
        public abstract void BindData(T bindObject);

        public abstract void PreSave();
        public abstract void Close();
        public abstract void Hide();

        public void Save()
        {
            for (int i = 0; i < UpperBound; i++)
            {
                
            }
            PreSave();
            BindingProvider().Save();
            OnChanged(this, EventArgs.Empty);
            BindingProvider().Release();
            try
            {
               Close();
            }
            catch (Exception ex)
            {
                Hide();
            }
        }

        public void OK(object sender, EventArgs e)
        {
            Save();
        }

        public void Cancel(object sender, EventArgs e)
        {
            BindingProvider().Release();
            Close();
        }
    }

   
}
