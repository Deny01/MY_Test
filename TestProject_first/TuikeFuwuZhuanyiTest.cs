using HappyYF.YuanXin.WorkSet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestProject_first
{
    
    
    /// <summary>
    ///这是 TuikeFuwuZhuanyiTest 的测试类，旨在
    ///包含所有 TuikeFuwuZhuanyiTest 单元测试
    ///</summary>
    [TestClass()]
    public class TuikeFuwuZhuanyiTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试属性
        // 
        //编写测试时，还可使用以下属性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///InitializeComponent 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HappyYF.YuanXin.dll")]
        public void InitializeComponentTest()
        {
            // 为“Microsoft.VisualStudio.TestTools.TypesAndSymbols.Assembly”创建专用访问器失败
            Assert.Inconclusive("为“Microsoft.VisualStudio.TestTools.TypesAndSymbols.Assembly”创建专用访问器失败");
        }

        /// <summary>
        ///Dispose 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HappyYF.YuanXin.dll")]
        public void DisposeTest()
        {
            // 为“Microsoft.VisualStudio.TestTools.TypesAndSymbols.Assembly”创建专用访问器失败
            Assert.Inconclusive("为“Microsoft.VisualStudio.TestTools.TypesAndSymbols.Assembly”创建专用访问器失败");
        }

        /// <summary>
        ///TuikeFuwuZhuanyi 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void TuikeFuwuZhuanyiConstructorTest()
        {
            TuikeFuwuZhuanyi target = new TuikeFuwuZhuanyi();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
    }
}
