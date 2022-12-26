using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using static dio_gtksharp_banktransfer.utils;

namespace dio_gtksharp_banktransfer
{
    class WCad : Window
    {
        [UI] private Gtk.Entry _eNome = null;
        [UI] private Gtk.Entry _eSaldo = null;
        [UI] private Gtk.Entry _eCred = null;
        [UI] private Gtk.RadioButton _rbFis = null;
        // [UI] private Gtk.RadioButton _rbJur = null;
        [UI] private Gtk.Button _btsv = null;
        public WCad() : this(new Builder("WCad.glade")) {}

        private WCad(Builder builder) : base(builder.GetRawOwnedObject("WCad"))
        {
            builder.Autoconnect(this);

            _btsv.Clicked += Bt_Clicked;
            _rbFis.Active = true;
        }

        private bool conds(){
            if (!double.TryParse(_eSaldo.Text, out var _)) {msgbox("Valor inválido!", _Win: this); _eSaldo.GrabFocus(); return false;}
            if (!double.TryParse(_eCred.Text, out var _)) {msgbox("Valor inválido!", _Win: this); _eCred.GrabFocus(); return false;}
            return true;
        }

        private void Bt_Clicked(object sender, EventArgs a) {
            if (!conds()) return;
            var cta = new Conta(_eNome.Text,
                                (_rbFis.Active==true ? TipoConta.PessoaFisica : TipoConta.PessoaJuridica),
                                double.Parse(_eSaldo.Text),
                                double.Parse(_eCred.Text));
            dados.CadastraConta(cta);
            this.Destroy();
        }
    }
}