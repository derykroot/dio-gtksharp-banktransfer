using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace dio_gtksharp_banktransfer
{
    class WDepSac : Window
    {
        private bool modoSacar;
        [UI] private Gtk.Button _bt = null;
        [UI] private Gtk.Label _lbop = null;
        [UI] private Gtk.Label _lbsld = null;
        [UI] private Gtk.Entry _eCod = null;
        [UI] private Gtk.Entry _eValor= null;
        private Conta cta;
        public WDepSac() : this(new Builder("WDepSac.glade")) {}

        public void Init(bool modoSacar = false) {
            this.modoSacar = modoSacar;
            if (modoSacar) {
                _bt.Label = "Sacar";
                _lbop.Text = "Sacar";
            }
            this.Show();

            // _eCod.Changed += eleave;
            _eCod.StateChanged += new StateChangedHandler((object sender, StateChangedArgs args) => {
                int cod;
                if (int.TryParse(_eCod.Text, out cod)) {
                    cta = dados.GetConta(cod);
                    _lbsld.Text= cta==null ? "" : string.Format("{0:#.00}", cta.Saldo);
                }
            });
        }

        private WDepSac(Builder builder) : base(builder.GetRawOwnedObject("WDepSac"))
        {
            builder.Autoconnect(this);

            _bt.Clicked += Bt_Clicked;
        }
        private bool conds {
            get {
                if (cta == null) {utils.msgbox("Conta inválida", _Win: this); _eCod.GrabFocus(); return false;}
                if (!double.TryParse(_eValor.Text, out double _)) {utils.msgbox("Valor inválido", _Win: this); _eValor.GrabFocus(); return false;}
                
                return true;
            }
        }


        private void Bt_Clicked(object sender, EventArgs a) {
            if (!conds) return;

            var vlr = double.Parse(_eValor.Text);

            if (!modoSacar) cta.Depositar(vlr);
            else if (cta.Sacar(vlr) == SaqueRes.SemCred) {
                utils.msgbox("Crédito Insuficiente!", _Win: this);
                _eValor.GrabFocus();
                return;
            }            

            dados.updateStore(cta.Cod, 3, cta.Saldo);    

            this.Destroy();
        }
    }
}
