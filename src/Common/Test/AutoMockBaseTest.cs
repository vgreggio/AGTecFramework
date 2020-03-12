﻿using AutoMoqCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace AGTec.Common.Test
{
    public abstract class AutoMockBaseTest<TSut, TContract>
           : BaseTestWithSut<TContract> where TSut : class, TContract
    {
        protected AutoMoqer AutoMocker { get; private set; }

        protected TSut ConcreteSut => (TSut)this.Sut;

        public Mock<T> Dep<T>() where T : class
        {
            return AutoMocker.GetMock<T>();
        }

        protected override TContract CreateSut()
        {
            if (typeof(TSut).IsInterface)
                throw new NotSupportedException(
                    "You can not use interfaces as SUT. Please use concrete class implementations.");

            return AutoMocker.Resolve<TSut>();
        }

        protected override void BeforeEachTest()
        {
            base.BeforeEachTest();

            AutoMocker = new AutoMoqer();
        }

        protected void AssertDependencyInjection<TResult>(Func<TContract, TResult> func)
            where TResult : class
        {
            Assert.AreSame(Dep<TResult>(), func(this.Sut));
        }
    }
}
