using System;
using System.Reflection;
using System.Reflection.Emit;

namespace MonkeyBalls
{
    public static class MB
    {
        const string monkeyTail = "_MonkeyTail_";

        /// <summary>Sets the value of the object contained in the generated monkey ball and sets the field containing the monkey ball.</summary>
        /// <param name="owner">The object containing the field containing the monkey ball.</param>
        /// <param name="fieldName">The name of the field containing the monkey ball.</param>
        /// <param name="val">to set the monkey ball to.</param>
        public static void SetMonkeyBalls(object owner, string fieldName, object val) {
            FieldInfo field = owner.GetType().GetField(fieldName,BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance);
            field.SetValue(owner, SetMonkeyBalls(field.GetValue(owner), val));
        }

        /// <summary>Sets the value of the object contained in the generated monkey ball.</summary>
        /// <param name="host">The object the monkey ball is embedded in.</param>
        /// <param name="val">to set the monkey ball to.</param>
        /// <returns>The updated monkey ball.</returns>
        public static T SetMonkeyBalls<T>(T host, object val) {
            if(!(host is ImAMonkeyBall)) {
                AttachMonkeyBall(ref host);
            }
            FieldInfo f = host.GetType().GetField(monkeyTail, BindingFlags.Public | BindingFlags.Instance);
            f.SetValue(host, val);
            return host;
        }

        /// <summary>Gets the value of the object contained in the generated monkey ball.</summary>
        /// <param name="host">The object the monkey ball is embedded in.</param>
        /// <returns>the value of the object contained in the generated monkey ball.</returns>
        public static T GetMonkeyBalls<T>(object host) {
            Type hostType = host.GetType();
            if(!(host is ImAMonkeyBall)) {
                return default(T);
            }
            return (T)hostType.GetField(monkeyTail, BindingFlags.Public | BindingFlags.Instance).GetValue(host);
        }
        private static void AttachMonkeyBall<T>(ref T host) {
            Type hostType = host.GetType();
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("DynamicMonkeyBallsASM"), AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicMonkeyBallsMOD");
            TypeBuilder typeBuilder= moduleBuilder.DefineType(
                hostType.Name+"MonkeyBall",
                TypeAttributes.Public
                | TypeAttributes.Class
                | TypeAttributes.AutoClass
                | TypeAttributes.AnsiClass
                | TypeAttributes.BeforeFieldInit,
                hostType,new Type[]{typeof(ImAMonkeyBall)});
            typeBuilder.DefineField(
                    monkeyTail,
                    typeof(object),
                    FieldAttributes.Public);
            Type monkeyType = typeBuilder.CreateType();
            object monkeyBall = Activator.CreateInstance(monkeyType);
            foreach(var sourceProperty in hostType.GetProperties()) {
                var targetProperty = monkeyType.GetProperty(sourceProperty.Name);
                targetProperty.SetValue(monkeyBall, sourceProperty.GetValue(host));
            }
            foreach(var sourceField in hostType.GetFields()) {
                var targetField = monkeyType.GetField(sourceField.Name);
                targetField.SetValue(monkeyBall, sourceField.GetValue(host));
            }
            host = (T)monkeyBall;
        }
    }
    public interface ImAMonkeyBall { }
}
