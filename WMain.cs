using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace dio_gtksharp_banktransfer
{
    class WMain : Window
    {   
        [UI] private Gtk.ImageMenuItem _mbtabout= null;
        [UI] private Gtk.ImageMenuItem _mbtsair= null;
        [UI] private Gtk.MenuItem _mbtcad= null;
        [UI] private Gtk.MenuItem _mbttransf= null;

        [UI] private Gtk.MenuItem _mbtsac= null;
        [UI] private Gtk.MenuItem _mbtdep= null;
        [UI] private Gtk.Box _clbox= null;

        private CustomTreeView _tv = new CustomTreeView();

        public WMain() : this(new Builder("WMain.glade")) { }

        private WMain(Builder builder) : base(builder.GetRawOwnedObject("WMain"))
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            _mbtabout.ButtonReleaseEvent += BtAbout_Clicked;
            _mbtcad.ButtonReleaseEvent += new ButtonReleaseEventHandler((object sender, ButtonReleaseEventArgs a) => (new WCad()).Show());
            _mbttransf.ButtonReleaseEvent += new ButtonReleaseEventHandler((object sender, ButtonReleaseEventArgs a) => (new WTransf()).Show());
            _mbtsac.ButtonReleaseEvent += new ButtonReleaseEventHandler((object sender, ButtonReleaseEventArgs a) => (new WDepSac()).Init(true));
            _mbtdep.ButtonReleaseEvent += new ButtonReleaseEventHandler((object sender, ButtonReleaseEventArgs a) => (new WDepSac()).Init());
            _mbtsair.ButtonReleaseEvent += new ButtonReleaseEventHandler((object sender, ButtonReleaseEventArgs a) => Application.Quit());

            _tv.AddCol("Cod").MinWidth = 70;    
            _tv.AddCol("Nome").MinWidth = 150;
            _tv.AddCol("Tipo").MinWidth = 40;
            _tv.AddCol("Saldo", iscoinfield: true).MinWidth = 100;
            _tv.AddCol("Crédito", iscoinfield: true).MinWidth = 100;
            _clbox.PackStart(_tv, true, true, 0);
            _tv.Show();

            this.LoadData();
        }

        private void LoadData() {
            _tv.Model = dados.modelStore; // dataStore;

            dados.CadastraConta(new Conta("Fulano", TipoConta.PessoaFisica, 100, 200));
            dados.CadastraConta(new Conta("Ciclano", TipoConta.PessoaJuridica, 300, 500));
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void Btcad_Clicked(object sender, EventArgs a)
        {
            var _w = new WCad();
            _w.Show();
        }
        private void BtAbout_Clicked(object sender, EventArgs a)
        {
            // _clbox.Child = bt;
            // _clbox.Add(bt);
            // _clbox.PackStart(bt, true, true, 10);
            // bt.Show();
            (new WSobre()).Show();
            
            //dados.updateStore(2, 3, 50.555);            
        }
    }
}
