using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace ConfigRepository.Tests.EditMode
{
    internal sealed class TestConfig : Config<int>
    {
        [SerializeField]
        private string _value = string.Empty;

        public string Value => _value;
    }

    internal sealed class TestRepository : ConfigRepository<int, TestConfig>
    {
    }

    public sealed class ConfigRepositoryTests
    {
        private static readonly BindingFlags PrivateInstance = BindingFlags.Instance | BindingFlags.NonPublic;

        [Test]
        public void TrySet_StoresConfig_AndSynchronizesId()
        {
            TestRepository repository = ScriptableObject.CreateInstance<TestRepository>();
            TestConfig config = new TestConfig();

            bool result = repository.TrySet(10, config);

            Assert.That(result, Is.True);
            Assert.That(repository.TryGet(10, out TestConfig storedConfig), Is.True);
            Assert.That(storedConfig, Is.SameAs(config));
            Assert.That(config.Id, Is.EqualTo(10));
        }

        [Test]
        public void TryRemove_RemovesStoredConfig()
        {
            TestRepository repository = ScriptableObject.CreateInstance<TestRepository>();
            TestConfig config = new TestConfig();

            repository.TrySet(20, config);

            bool removed = repository.TryRemove(20);

            Assert.That(removed, Is.True);
            Assert.That(repository.TryGet(20, out _), Is.False);
        }

        [Test]
        public void Clear_EmptiesRepository()
        {
            TestRepository repository = ScriptableObject.CreateInstance<TestRepository>();
            TestConfig config = new TestConfig();

            repository.TrySet(30, config);
            repository.Clear();

            Assert.That(repository.TryGet(30, out _), Is.False);
        }

        [Test]
        public void OnValidate_ReconcilesMismatchedConfigId()
        {
            TestRepository repository = ScriptableObject.CreateInstance<TestRepository>();
            TestConfig config = new TestConfig();

            repository.TrySet(40, config);
            SetPrivateId(config, 999);

            InvokeOnValidate(repository);

            Assert.That(config.Id, Is.EqualTo(40));
        }

        private static void SetPrivateId(TestConfig config, int id)
        {
            FieldInfo idField = typeof(Config<int>).GetField("_id", PrivateInstance);
            Assert.That(idField, Is.Not.Null);
            idField!.SetValue(config, id);
        }

        private static void InvokeOnValidate(TestRepository repository)
        {
            MethodInfo onValidateMethod = typeof(ConfigRepository<int, TestConfig>).GetMethod("OnValidate", PrivateInstance);
            Assert.That(onValidateMethod, Is.Not.Null);
            onValidateMethod!.Invoke(repository, null);
        }
    }
}
