using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace dio_gtksharp_banktransfer
{
    class WTransf : Window
    {
        [UI] private Gtk.Button _bt = null;
        public WTransf() : this(new Builder("WTransf.glade")) {}

        private WTransf(Builder builder) : base(builder.GetRawOwnedObject("WTransf"))
        {
            builder.Autoconnect(this);

            _bt.Clicked += Bt_Clicked;
        }

        private void Bt_Clicked(object sender, EventArgs a) {
            this.Destroy();
        }
    }
}
