

namespace PlayerInput{
    using InControl;
    using UnityEngine;


    public class InputBindings : PlayerActionSet
    {
        public PlayerAction Left;
        public PlayerAction Right;
        public PlayerAction SlowDown;
        public PlayerAction SpeedUp;
        public PlayerAction SlowSpeed;
        public PlayerAction NormalSpeed;
        public PlayerAction FastSpeed;
        public PlayerAction Pause;



        public InputBindings()
        {
            Left = CreatePlayerAction("Move Left");
            Right = CreatePlayerAction("Move Right");
            SlowDown = CreatePlayerAction("Slow Down");
            SpeedUp = CreatePlayerAction("Speed Up");
            SlowSpeed = CreatePlayerAction("Slow Speed");
            NormalSpeed = CreatePlayerAction("Normal Speed");
            FastSpeed = CreatePlayerAction("Fast Speed");
            Pause = CreatePlayerAction("Pause");
        }


        public static InputBindings CreateWithDefaultBindings()
        {
            var playerActions = new InputBindings();

            // How to set up mutually exclusive keyboard bindings with a modifier key.
            // playerActions.Back.AddDefaultBinding( Key.Shift, Key.Tab );
            // playerActions.Next.AddDefaultBinding( KeyCombo.With( Key.Tab ).AndNot( Key.Shift ) );

            playerActions.Left.AddDefaultBinding(Key.LeftArrow);
            playerActions.Right.AddDefaultBinding(Key.RightArrow);

            playerActions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
            playerActions.Right.AddDefaultBinding(InputControlType.LeftStickRight);


            playerActions.Left.AddDefaultBinding(InputControlType.DPadLeft);
            playerActions.Right.AddDefaultBinding(InputControlType.DPadRight);



            playerActions.Left.AddDefaultBinding(Mouse.NegativeX);
            playerActions.Right.AddDefaultBinding(Mouse.PositiveX);

            playerActions.ListenOptions.IncludeUnknownControllers = true;
            playerActions.ListenOptions.MaxAllowedBindings = 4;
            playerActions.ListenOptions.UnsetDuplicateBindingsOnSet = true;


            playerActions.ListenOptions.OnBindingFound = (action, binding) => {
                if (binding == new KeyBindingSource(Key.Escape))
                {
                    action.StopListeningForBinding();
                    return false;
                }
                return true;
            };

            playerActions.ListenOptions.OnBindingAdded += (action, binding) => {
                Debug.Log("Binding added... " + binding.DeviceName + ": " + binding.Name);
            };

            playerActions.ListenOptions.OnBindingRejected += (action, binding, reason) => {
                Debug.Log("Binding rejected... " + reason);
            };

            return playerActions;
        }
    }
}
