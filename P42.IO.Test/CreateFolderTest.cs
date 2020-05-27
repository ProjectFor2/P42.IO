using Microsoft.VisualStudio.TestTools.UnitTesting;
using P42.IO.Directory;
using System;
using System.Activities;
using System.Activities.Statements;
using System.IO;
using System.Text;

namespace P42.IO.Test
{
    [TestClass]
    public class CreateFolderTest
    {
        private const string PATH = "P42path";

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CreateDirectoryArgumentExceptionTest() => CreateFolderSuccessTest(string.Empty);

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateDirectoryArgumentNullExceptionTest() => CreateFolderSuccessTest(null);

        [TestMethod, ExpectedException(typeof(DirectoryNotFoundException))]
        public void CreateDirectoryDirectoryNotFoundExceptionTest() => CreateFolderSuccessTest($@"a:\{PATH}");

        [TestMethod, ExpectedException(typeof(IOException))]
        public void CreateDirectoryIOExceptionTest()
        {
            const string fileName = "txt-test.txt";
            var fullName = $@"{PATH}\{fileName}";

            CreateFolderSuccessTest(PATH);

            if (!File.Exists(fullName))
                File.Create(fullName);

            CreateFolderSuccessTest(fullName);
        }

        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void CreateDirectoryNotSupportedExceptionTest() => CreateFolderSuccessTest($@"{PATH}:");

        [TestMethod, ExpectedException(typeof(PathTooLongException))]
        public void CreateDirectoryPathTooLongExceptionTest()
        {
            var longPath = new StringBuilder();
            longPath.Append(PATH);

            for (var i = 0; i < 260; i++)
                longPath.Append("p");

            CreateFolderSuccessTest(longPath.ToString());
        }

        [TestMethod]
        public void CreateDirectorySuccessTest()
        {
            CreateFolderSuccessTest(PATH);

            var result = System.IO.Directory.Exists(PATH);
            Assert.IsTrue(result);
        }

        private static void CreateFolderSuccessTest(string path)
        {
            var pathInfo = new Variable<DirectoryInfo> { Name = "PathInfo" };

            var workflow = new Sequence
            {
                Activities = {
                    new CreateFolder
                    {
                        Path = new InArgument<string>(path),
                        PathInfo = new OutArgument<DirectoryInfo>(pathInfo)
                    },
                    new WriteLine
                    {
                        Text = new InArgument<string>(ctx => $"Caminho: {pathInfo.Get(ctx).FullName}")
                    }
                },

                Variables =
                {
                    pathInfo
                }
            };

            WorkflowInvoker.Invoke(workflow);
        }
    }
}
