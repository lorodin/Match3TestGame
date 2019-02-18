using Math3TestGame.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Controllers
{
    public delegate void ChangeController(ControllerNames name);

    public abstract class Controller
    {
        public event ChangeController OnChangeController;
        protected IRenderer renderer;
        public ControllerNames CName { get; private set; }

        public Controller(ControllerNames name)
        {
            CName = name;
        }

        public abstract void Update(int dt);

        public abstract void MouseClick(int x, int y);
        public abstract void MouseMove(int x, int y);

        protected void ChangeController(ControllerNames name)
        {
            if (OnChangeController != null) OnChangeController(name);
        }

        public IRenderer GetRenderer()
        {
            return renderer;
        }
    }
    public enum ControllerNames
    {
        Start = 0,
        Play = 1
    }
}
