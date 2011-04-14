﻿#region Copyright (c) Lokad 2009-2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System.IO;
using Autofac;
using Lokad.Cloud.Storage;
using Lokad.Cloud.Storage.Shared;
using NUnit.Framework;

namespace Lokad.Cloud.Test.Storage
{
    [TestFixture]
    public class MessageWrapperTests
    {
        [Test]
        public void Serialization()
        {
            // overflowing message
            var om = new MessageWrapper {ContainerName = "con", BlobName = "blo"};

            var stream = new MemoryStream();
            var serializer = GlobalSetup.Container.Resolve<IDataSerializer>();

            serializer.Serialize(om, stream);
            stream.Position = 0;
            var omBis = (MessageWrapper) serializer.Deserialize(stream, typeof (MessageWrapper));

            Assert.AreEqual(om.ContainerName, omBis.ContainerName, "#A00");
            Assert.AreEqual(om.BlobName, omBis.BlobName, "#A01");
        }
    }
}
