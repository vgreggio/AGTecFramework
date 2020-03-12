using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGTec.Common.Test
{
    [TestClass]
    public abstract class BaseTestWithSut<TSut>
            : BaseTest
    {

        protected TSut Sut { get; set; }

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            this.BeforeCreateSut();

            this.Sut = this.CreateSut();

            this.AfterCreateSut();
        }

        protected virtual void BeforeCreateSut()
        {
        }

        protected virtual void AfterCreateSut()
        {
        }

        protected abstract TSut CreateSut();
    }
}
