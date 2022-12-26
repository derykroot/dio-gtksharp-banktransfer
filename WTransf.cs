using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace dio_gtksharp_banktransfer
{
    class WTransf : Window
    {
        [UI] private Gtk.Button _bt = null;
        [UI] private Gtk.Entry _eCodorig = null;
        [UI] private Gtk.Entry _eCoddest = null;
        [UI] private Gtk.Entry _eValor= null;
        
        public WTransf() : this(new Builder("WTransf.glade")) {}

        private Conta ctao;
        private Conta ctad;
        private WTransf(Builder builder) : base(builder.GetRawOwnedObject("WTransf"))
        {
            builder.Autoconnect(this);

            _bt.Clicked += Bt_Clicked;
        }

        private bool conds {
            get {
                int codo;
                int codd;
                if (!int.TryParse(_eCodorig.Text, out codo)) {utils.msgbox("Valor inválido", _Win: this); _eCodorig.GrabFocus(); return false;}
                if (!int.TryParse(_eCoddest.Text, out codd)) {utils.msgbox("Valor inválido", _Win: this); _eCoddest.GrabFocus(); return false;}

                ctao = dados.GetConta(codo);
                ctad = dados.GetConta(codd);

                if (ctao == null) {utils.msgbox("Conta inválida", _Win: this); _eCodorig.GrabFocus(); return false;}
                if (ctad == null) {utils.msgbox("Conta inválida", _Win: this); _eCoddest.GrabFocus(); return false;}
                if (!double.TryParse(_eValor.Text, out double _)) {utils.msgbox("Valor inválido", _Win: this); _eValor.GrabFocus(); return false;}
                
                return true;
            }
        }

        private void Bt_Clicked(object sender, EventArgs a) {
            if (!conds) return;

            if (ctao.Transferir(double.Parse(_eValor.Text), ctad) == SaqueRes.SemCred) {
                utils.msgbox("Crédito Insuficiente!", _Win: this);
                _eValor.GrabFocus();
                return;
            }    

            dados.updateStore(ctao.Cod, 3, ctao.Saldo);    
            dados.updateStore(ctad.Cod, 3, ctad.Saldo);    

            this.Destroy();
        }
    }
}
