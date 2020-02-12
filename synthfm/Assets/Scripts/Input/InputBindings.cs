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

        // Menu Actions
        public PlayerAction Menu_Down;
        public PlayerAction Menu_Up;
        public PlayerAction Menu_Select;

        public PlayerAction Screen_Touch;

        //Debug
        public PlayerAction Debug_Puzzle_Complete;

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

            // Menu actions
            Menu_Down = CreatePlayerAction("Scroll Down");
            Menu_Up = CreatePlayerAction("Scroll Up");
            Menu_Select = CreatePlayerAction("Select");

            Debug_Puzzle_Complete = CreatePlayerAction("Debug Puzzle Complete");

            Screen_Touch = CreatePlayerAction("Screen Touch");
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
            playerActions.Pause.AddDefaultBinding(InputControlType.Menu);

            playerActions.SlowSpeed.AddDefaultBinding(Key.Key1);
            playerActions.NormalSpeed.AddDefaultBinding(Key.Key2);
            playerActions.FastSpeed.AddDefaultBinding(Key.Key3);

            playerActions.Menu_Up.AddDefaultBinding(InputControlType.LeftStickUp);
            playerActions.Menu_Up.AddDefaultBinding(Key.W);
            playerActions.Menu_Up.AddDefaultBinding(Key.UpArrow);
            playerActions.Menu_Down.AddDefaultBinding(InputControlType.LeftStickDown);
            playerActions.Menu_Down.AddDefaultBinding(Key.S);
            playerActions.Menu_Down.AddDefaultBinding(Key.DownArrow);
            playerActions.Menu_Select.AddDefaultBinding(InputControlType.Action1);
            playerActions.Menu_Select.AddDefaultBinding(Key.Return);
            playerActions.Menu_Select.AddDefaultBinding(Key.Space);

            playerActions.Debug_Puzzle_Complete.AddDefaultBinding(Key.Space);
            playerActions.Debug_Puzzle_Complete.AddDefaultBinding(InputControlType.Action2);

            playerActions.Screen_Touch.AddDefaultBinding(Mouse.LeftButton);

            playerActions.ListenOptions.IncludeUnknownControllers = true;
            playerActions.ListenOptions.MaxAllowedBindings = 4;
            playerActions.ListenOptions.UnsetDuplicateBindingsOnSet = true;


            return playerActions;
        }
    }
}
