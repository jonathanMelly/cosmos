using System;
using System.IO;
using System.IO.Compression;
using FluentAssertions;
using lib;
using Xunit;
using OperatingSystem = lib.OperatingSystem;

namespace test
{
    public class TestUpdater : IDisposable
    {
        private static int id = 0; //used to avoid parallel issues
        private Updater updater;
        private string httpResult;
        private string binDirectory;
        private string appPath;
        
        private static string ORIGINAL_CONTENT = "previousVersion";

        public TestUpdater()
        {
            
            httpResult = File.ReadAllText(TestUtils.ToDataPath(@"releaseApiVersionResult.txt"));
            binDirectory = TestUtils.ToDataPath($"bin{id++}");
            appPath = Path.Combine(binDirectory, "cosmos.exe");

            if (Directory.Exists(binDirectory))
            {
                Dispose();
            }
            Directory.CreateDirectory(binDirectory);
            File.WriteAllText(appPath, ORIGINAL_CONTENT);
            
        }

        public void Dispose()
        {
            foreach (var file in Directory.GetFiles(binDirectory))
            {
                File.Delete(file);
            }

            Directory.Delete(binDirectory);
        }


        [Fact]
        public void TestUpdateAvailable()
        {
            //Arrange
            updater = new Updater($"1.1.0", appPath);
            updater.HttpResult = httpResult;

            //Act
            updater.IsUpdateAvailable();

            //Assert
            updater.LatestVersion.Should().Be("1.13.1");
        }

        [Fact]
        public void TestUpdateUnnecessary()
        {
            //Arrange
            updater = new Updater("1.13.1", appPath);
            updater.HttpResult = httpResult;

            //Act
            var result = updater.DoUpdate();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void TestUpdateNecessary()
        {
            //Arrange
            updater = new Updater("1.12.1", appPath);
            updater.HttpResult = httpResult;
            var newContent = new[] {(byte) 't', (byte) 'e', (byte) 's', (byte) 't'};

            //https://stackoverflow.com/questions/17232414/creating-a-zip-archive-in-memory-using-system-io-compression
            string fileName = "cosmos.exe";
            using (var outStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                {
                    var fileInArchive = archive.CreateEntry(fileName, CompressionLevel.Optimal);
                    using (var entryStream = fileInArchive.Open())
                    using (var fileToCompressStream = new MemoryStream(newContent))
                    {
                        fileToCompressStream.CopyTo(entryStream);
                    }
                }
                updater.NewExecutableContent = outStream.ToArray();
            }

            //Act
            var result = updater.DoUpdate();

            //Assert
            result.Should().BeTrue("Updater should report successfull update");
            File.ReadAllBytes(appPath).Should().BeEquivalentTo(newContent);
        }
        
        [Fact]
        public void TestUpdateNecessaryBadArchive()
        {
            //Arrange
            updater = new Updater("1.12.1", appPath);
            updater.HttpResult = httpResult;
            var newContent = new[] {(byte) 't', (byte) 'e', (byte) 's', (byte) 't'};

            //https://stackoverflow.com/questions/17232414/creating-a-zip-archive-in-memory-using-system-io-compression
            string fileName = "2cosmos.exe";
            using (var outStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                {
                    var fileInArchive = archive.CreateEntry(fileName, CompressionLevel.Optimal);
                    using (var entryStream = fileInArchive.Open())
                    using (var fileToCompressStream = new MemoryStream(newContent))
                    {
                        fileToCompressStream.CopyTo(entryStream);
                    }
                }
                updater.NewExecutableContent = outStream.ToArray();
            }

            //Act
            Action act = () => updater.DoUpdate();

            //Assert
            var platformIdentifier = OperatingSystem.GetPlatformIdentifier();
            act.Should().Throw<ApplicationException>("Updater should report unsuccessfull update").WithMessage($"Archive provenant de https://github.com/jonathanMelly/cosmos/releases/latest/download/cosmos-{platformIdentifier}-x64.zip invalide");
            File.ReadAllBytes(appPath).Should().NotBeEquivalentTo(newContent,"new version should not have replaced original version");
            File.ReadAllText(appPath).Should().BeEquivalentTo(ORIGINAL_CONTENT,"original version should have been put back");
        }
    }
}