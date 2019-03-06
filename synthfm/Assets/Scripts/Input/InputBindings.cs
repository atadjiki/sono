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
            Left = CreatePlayerAction("Turn Left");
            Right = CreatePlayerAction("Turn Right");
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
            playerActions.Left.AddDefaultBinding(Key.A);
            playerActions.Right.AddDefaultBinding(Key.RightArrow);
            playerActions.Right.AddDefaultBinding(Key.D);

            playerActions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
            playerActions.Right.AddDefaultBinding(InputControlType.LeftStickRight);

            playerActions.Left.AddDefaultBinding(InputControlType.RightStickLeft);
            playerActions.Right.AddDefaultBinding(InputControlType.RightStickRight);

            playerActions.SlowDown.AddDefaultBinding(Key.S);
            playerActions.SlowDown.AddDefaultBinding(InputControlType.LeftTrigger);

            playerActions.SpeedUp.AddDefaultBinding(Key.W);
            playerActions.SpeedUp.AddDefaultBinding(InputControlType.RightTrigger);

            playerActions.Pause.AddDefaultBinding(Key.Escape);
            playerActions.Pause.AddDefaultBinding(InputControlType.Start);

            playerActions.SlowSpeed.AddDefaultBinding(Key.Key1);
            playerActions.NormalSpeed.AddDefaultBinding(Key.Key2);
            playerActions.FastSpeed.AddDefaultBinding(Key.Key3);


            playerActions.ListenOptions.IncludeUnknownControllers = true;
            playerActions.ListenOptions.MaxAllowedBindings = 4;
            playerActions.ListenOptions.UnsetDuplicateBindingsOnSet = true;


            return playerActions;
        }
    }
}
