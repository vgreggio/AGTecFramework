using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AGTec.Common.Test
{
    public abstract class AutoMockSpecification<TSut, TContract>
            : AutoMockBaseTest<TSut, TContract> where TSut : class, TContract
    {

        protected Exception ExceptionThrown { get; set; }

        protected bool IsCreation { get; set; }

        protected bool CatchExceptionOnRunning { get; set; }

        protected AutoMockSpecification()
        {
            this.CatchExceptionOnRunning = this.HasAttribute<ExceptionSpecificationAttribute>();

            this.IsCreation = this.HasAttribute<ConstructorSpecificationAttribute>();
        }

        protected override void BeforeCreateSut()
        {
            this.GivenThat();
        }

        protected override void AfterCreateSut()
        {
            this.AndGivenThatAfterCreated();

            if (this.IsCreation)
            {
                return;
            }

            Action whenIRun = this.WhenIRun;

            if (this.CatchExceptionOnRunning)
            {
                whenIRun = () => this.RegisterException(this.WhenIRun);
            }

            whenIRun();
        }

        protected override void AfterEachTest()
        {
            this.AndThenCleanUp();
        }

        protected virtual void GivenThat()
        {
        }

        protected virtual void AndGivenThatAfterCreated()
        {
        }

        protected virtual void WhenIRun()
        {
            throw new NotImplementedException("Please implement WhenIRun in the derived class or use ConstructorSpecification attrubute in the class");
        }

        protected virtual void AndThenCleanUp()
        {
        }

        protected void RegisterException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                this.ExceptionThrown = e;
            }
        }

        protected void AssertExceptionThrown()
        {
            AssertExceptionThrown<Exception>();
        }

        protected void AssertExceptionThrown<T>() where T : Exception
        {
            Assert.IsInstanceOfType(this.ExceptionThrown, typeof(T));
        }

        protected bool HasAttribute<T>() where T : Attribute
        {
            var attributes = this.GetType().GetCustomAttributes(typeof(T), true);

            return attributes.Length > 0;
        }
    }
}
