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
    public class DeleteFolderTest
    {
        private const string PATH = "P42path";

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DeleteDirectoryArgumentExceptionTest() => DeleteFolderSuccessTest(string.Empty);

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteDirectoryArgumentNullExceptionTest() => DeleteFolderSuccessTest(null);

        [TestMethod, ExpectedException(typeof(DirectoryNotFoundException))]
        public void DeleteDirectoryDirectoryNotFoundExceptionTest() => System.IO.Directory.SetCurrentDirectory($@"PATH\{PATH}_temp");

        [TestMethod, ExpectedException(typeof(IOException))]
        public void DeleteDirectoryIOExceptionTest()
        {
            const string fileName = "txt-test.txt";
            var fullName = $@"{PATH}\{fileName}";

            System.IO.Directory.CreateDirectory(PATH);
            TestHelper.CreateFile(fullName);
            DeleteFolderSuccessTest(fullName);
        }

        [TestMethod, ExpectedException(typeof(PathTooLongException))]
        public void DeleteDirectoryPathTooLongExceptionTest()
        {
            var longPath = new StringBuilder();
            longPath.Append(PATH);

            for (var i = 0; i < 260; i++)
                longPath.Append("p");

            DeleteFolderSuccessTest(longPath.ToString());
        }

        [TestMethod]
        public void DeleteDirectoryRecursiveSuccessTest()
        {
            const string fileName = "txt-test.txt";
            var fullName = $@"{PATH}\{fileName}";

            System.IO.Directory.CreateDirectory(PATH);
            TestHelper.CreateFile(fullName);

            DeleteFolderSuccessTest(PATH, true);

            var result = System.IO.Directory.Exists(PATH);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteDirectorySuccessTest()
        {
            System.IO.Directory.CreateDirectory(PATH);

            DeleteFolderSuccessTest(PATH);

            var result = System.IO.Directory.Exists(PATH);
            Assert.IsFalse(result);
        }

        private static void DeleteFolderSuccessTest(string path, bool recursive = false)
        {
            var workflow = new Sequence
            {
                Activities = {
                    new DeleteFolder
                    {
                        ContinueOnError = new InArgument<bool>(false),
                        Path = new InArgument<string>(path),
                        Recursive = recursive
                    },
                    new WriteLine
                    {
                        Text = new InArgument<string>(ctx => $"Pasta: {PATH} excluída com sucesso!")
                    }
                }
            };

            WorkflowInvoker.Invoke(workflow);
        }
    }
}
