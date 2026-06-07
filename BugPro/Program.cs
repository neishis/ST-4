using System;
using Stateless;

namespace BugPro
{
    public enum State
    {
        NewDefect,
        Triage,
        Fixing,
        Closed
    }

    public enum Trigger
    {
        Assign,
        StartFix,
        NotADefect,
        WontFix,
        Duplicate,
        NoTimeNow,
        NeedsSeparateSolution,
        OtherProductProblem,
        NeedMoreInfo,
        CannotReproduceAndOK,
        CannotReproduceAndNotOK,
        FixAccepted,
        FixRejected,
        Reopen
    }

    public class Bug
    {
        private readonly StateMachine<State, Trigger> _machine;

        public State CurrentState => _machine.State;

        public Bug()
        {
            _machine = new StateMachine<State, Trigger>(State.NewDefect);

            _machine.Configure(State.NewDefect)
                .Permit(Trigger.Assign, State.Triage);

            _machine.Configure(State.Triage)
                .Permit(Trigger.StartFix, State.Fixing)
                .Permit(Trigger.NotADefect, State.Closed)
                .Permit(Trigger.WontFix, State.Closed)
                .Permit(Trigger.Duplicate, State.Closed);

            _machine.Configure(State.Fixing)
                .Permit(Trigger.NoTimeNow, State.Triage)
                .Permit(Trigger.NeedsSeparateSolution, State.Triage)
                .Permit(Trigger.OtherProductProblem, State.Triage)
                .Permit(Trigger.NeedMoreInfo, State.Triage)
                .Permit(Trigger.CannotReproduceAndOK, State.Closed)
                .Permit(Trigger.CannotReproduceAndNotOK, State.Triage)
                .Permit(Trigger.FixAccepted, State.Closed)
                .Permit(Trigger.FixRejected, State.Triage);

            _machine.Configure(State.Closed)
                .Permit(Trigger.Reopen, State.Triage);
        }

        public void Fire(Trigger trigger)
        {
            _machine.Fire(trigger);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var bug = new Bug();
            Console.WriteLine($"Начальное состояние: {bug.CurrentState}");
            
            bug.Fire(Trigger.Assign);
            Console.WriteLine($"После назначения: {bug.CurrentState}");
            
            bug.Fire(Trigger.StartFix);
            Console.WriteLine($"Взято в работу: {bug.CurrentState}");
            
            bug.Fire(Trigger.FixAccepted);
            Console.WriteLine($"Проблема решена (ДА): {bug.CurrentState}");
        }
    }
}
