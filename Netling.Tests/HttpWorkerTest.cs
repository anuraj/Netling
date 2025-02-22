﻿using System;
using Netling.Core.SocketWorker.Performance;
using NUnit.Framework;

namespace Netling.Tests
{
    [TestFixture]  
    public class HttpWorkerTest
    {
        [Test]
        public void ReadResponse()
        {
            var client = (IHttpWorkerClient)new FakeHttpWorkerClient("HTTP/1.1 200 OK\r\nDate: Wed, 06 Jul 2016 18:26:27 GMT\r\nContent-Length: 13\r\nContent-Type: text/plain\r\nServer: Kestrel\r\n\r\nHello, World!");
            var httpWorker = new HttpWorker(client, new Uri("http://netling.test", UriKind.Absolute));

            var (length, statusCode) = httpWorker.Send();
            Assert.That(statusCode, Is.EqualTo(200));
            Assert.That(length, Is.EqualTo(132));
        }
        
        [Test]
        public void ReadResponseSplit()
        {
            var client = (IHttpWorkerClient)new FakeHttpWorkerClient("HTTP/1.1 200 OK\r\nDate: Wed, 06 Jul 2016 18:26:27 GMT\r\nContent-Length: 13\r\nContent-Type: text/plain\r\nServer: Kestrel\r\n\r\n", "Hello, World!");
            var httpWorker = new HttpWorker(client, new Uri("http://netling.test", UriKind.Absolute));

            var (length, statusCode) = httpWorker.Send();
            Assert.That(statusCode, Is.EqualTo(200));
            Assert.That(length, Is.EqualTo(132));
        }
    }
}
