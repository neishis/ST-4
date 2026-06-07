using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugPro;
using Stateless;
namespace BugTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InitialState_IsNewDefect()
        {
            var bug = new Bug();
            Assert.AreEqual(State.NewDefect, bug.CurrentState);
        }
        [TestMethod]
        public void Assign_FromNewDefect_GoesToTriage()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            Assert.AreEqual(State.Triage, bug.CurrentState);
        }
        [TestMethod]
        public void StartFix_FromTriage_GoesToFixing()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.StartFix);
            Assert.AreEqual(State.Fixing, bug.CurrentState);
        }
        [TestMethod]
        public void NotADefect_FromTriage_GoesToClosed()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.NotADefect);
            Assert.AreEqual(State.Closed, bug.CurrentState);
        }
        [TestMethod]
        public void WontFix_FromTriage_GoesToClosed()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.WontFix);
            Assert.AreEqual(State.Closed, bug.CurrentState);
        }
        [TestMethod]
        public void Duplicate_FromTriage_GoesToClosed()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.Duplicate);
            Assert.AreEqual(State.Closed, bug.CurrentState);
        }
        [TestMethod]
        public void NoTimeNow_FromFixing_GoesToTriage()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.StartFix);
            bug.Fire(Trigger.NoTimeNow);
            Assert.AreEqual(State.Triage, bug.CurrentState);
        }
        [TestMethod]
        public void NeedsSeparateSolution_FromFixing_GoesToTriage()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.StartFix);
            bug.Fire(Trigger.NeedsSeparateSolution);
            Assert.AreEqual(State.Triage, bug.CurrentState);
        }
        [TestMethod]
        public void OtherProductProblem_FromFixing_GoesToTriage()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.StartFix);
            bug.Fire(Trigger.OtherProductProblem);
            Assert.AreEqual(State.Triage, bug.CurrentState);
        }
        [TestMethod]
        public void NeedMoreInfo_FromFixing_GoesToTriage()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.StartFix);
            bug.Fire(Trigger.NeedMoreInfo);
            Assert.AreEqual(State.Triage, bug.CurrentState);
        }
        [TestMethod]
        public void CannotReproduceAndOK_FromFixing_GoesToClosed()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.StartFix);
            bug.Fire(Trigger.CannotReproduceAndOK);
            Assert.AreEqual(State.Closed, bug.CurrentState);
        }
        [TestMethod]
        public void CannotReproduceAndNotOK_FromFixing_GoesToTriage()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.StartFix);
            bug.Fire(Trigger.CannotReproduceAndNotOK);
            Assert.AreEqual(State.Triage, bug.CurrentState);
        }
        [TestMethod]
        public void FixAccepted_FromFixing_GoesToClosed()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.StartFix);
            bug.Fire(Trigger.FixAccepted);
            Assert.AreEqual(State.Closed, bug.CurrentState);
        }
        [TestMethod]
        public void FixRejected_FromFixing_GoesToTriage()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.StartFix);
            bug.Fire(Trigger.FixRejected);
            Assert.AreEqual(State.Triage, bug.CurrentState);
        }
        [TestMethod]
        public void Reopen_FromClosed_GoesToTriage()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.NotADefect);
            bug.Fire(Trigger.Reopen);
            Assert.AreEqual(State.Triage, bug.CurrentState);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Invalid_StartFix_FromNewDefect_ThrowsException()
        {
            var bug = new Bug();
            bug.Fire(Trigger.StartFix);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Invalid_FixAccepted_FromNewDefect_ThrowsException()
        {
            var bug = new Bug();
            bug.Fire(Trigger.FixAccepted);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Invalid_Assign_FromTriage_ThrowsException()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.Assign);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Invalid_StartFix_FromFixing_ThrowsException()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.StartFix);
            bug.Fire(Trigger.StartFix);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Invalid_FixAccepted_FromClosed_ThrowsException()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.NotADefect);
            bug.Fire(Trigger.FixAccepted);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Invalid_StartFix_FromClosed_ThrowsException()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Assign);
            bug.Fire(Trigger.NotADefect);
            bug.Fire(Trigger.StartFix);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Invalid_Reopen_FromNewDefect_ThrowsException()
        {
            var bug = new Bug();
            bug.Fire(Trigger.Reopen);
        }
    }
}
