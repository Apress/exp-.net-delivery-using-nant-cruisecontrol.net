using System;
using NUnit.Framework;
using NUnit.Mocks;

namespace NUnit.Tests.Mocks
{
	/// <summary>
	/// Summary description for DynamicMockTests.
	/// </summary>
	[TestFixture]
	public class DynamicMockTests
	{
		private DynamicMock mock;
		private IStuff instance;

		[SetUp]
		public void CreateMock()
		{
			mock = new DynamicMock( typeof( IStuff ) );
			instance = (IStuff)mock.MockInstance;
		}

		[Test]
		public void MockHasDefaultName()
		{
			Assert.AreEqual( "MockIStuff", mock.Name );
		}

		[Test]
		public void MockHasNonDefaultName()
		{
			DynamicMock mock2 = new DynamicMock( "MyMock", typeof( IStuff ) );
			Assert.AreEqual( "MyMock", mock2.Name );
		}

		[Test]
		public void CallMethod()
		{
			instance.DoSomething();
			mock.Verify();
		}

		[Test]
		public void CallMethodWithArgs()
		{
			instance.DoSomething( "hello" );
			mock.Verify();
		}

		[Test]
		public void ExpectedMethod()
		{
			mock.Expect( "DoSomething" );
			instance.DoSomething();
			mock.Verify();
		}

		[Test, ExpectedException( typeof(AssertionException) )]
		public void ExpectedMethodNotCalled()
		{
			mock.Expect( "DoSomething" );
			mock.Verify();
		}

		[Test]
		public void MethodWithReturnValue()
		{
			mock.SetReturnValue( "GetInt", 5 );
			Assert.AreEqual( 5, instance.GetInt() );
			mock.Verify();
		}

		[Test]
		public void DefaultReturnValues()
		{
			Assert.AreEqual( 0, instance.GetInt(), "int" );
			Assert.AreEqual( 0, instance.GetSingle(), "float" );
			Assert.AreEqual( 0, instance.GetDouble(), "double" );
			Assert.AreEqual( 0, instance.GetDecimal(), "decimal" );
			Assert.AreEqual( '?', instance.GetChar(), "char" );
			mock.Verify();
		}

		[Test, ExpectedException( typeof(InvalidCastException) )]
		public void WrongReturnType()
		{
			mock.SetReturnValue( "GetInt", "hello" );
			instance.GetInt();
			mock.Verify();
		}

		[Test]
		public void OverrideMethodOnDynamicMock()
		{
			DynamicMock derivedMock = new DerivedMock();
			IStuff derivedInstance = (IStuff)derivedMock.MockInstance;
			Assert.AreEqual( 17, derivedInstance.Add( 5, 12 ) );
			derivedMock.Verify();
		}

		[Test, ExpectedException( typeof(ArgumentException) )]
		public void CreateMockForNonMBRClassFails()
		{
			DynamicMock classMock = new DynamicMock( typeof( NonMBRClass ) );
			NonMBRClass classInstance = (NonMBRClass)classMock.MockInstance;
		}

		[Test]
		public void CreateMockForMBRClass()
		{
			DynamicMock classMock = new DynamicMock( typeof( MBRClass ) );
			MBRClass classInstance = (MBRClass)classMock.MockInstance;
			classMock.Expect( "SomeMethod" );
			classMock.ExpectAndReturn( "AnotherMethod", "Hello World", 5, "hello" );
			classInstance.SomeMethod();
			Assert.AreEqual( "Hello World", classInstance.AnotherMethod( 5, "hello" ) );
			classMock.Verify();
		}

		#region Test Interfaces and Classes

		interface IStuff
		{
			void DoSomething();
			void DoSomething( string greeting );
			int GetInt();
			float GetSingle();
			double GetDouble();
			decimal GetDecimal();
			char GetChar();
			int Add( int a, int b );
		}

		class DerivedMock : DynamicMock
		{
			public DerivedMock() : base( "Derived", typeof( IStuff ) ) { }

			public override object Call( string methodName, params object[] args )
			{
				switch( methodName )
				{
					case "Add":
						return (int)args[0] + (int)args[1];
					default:
						return base.Call( methodName, args );
				}
			}
		}

		class NonMBRClass
		{
		}

		class MBRClass : MarshalByRefObject
		{
			public void SomeMethod(){ }
			public string AnotherMethod( int a, string b ) { return b + a.ToString(); }
		}

		#endregion
	}
}
