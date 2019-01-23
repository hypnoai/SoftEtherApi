using System;
using System.CodeDom;
using System.IO;
using System.Text;
using Microsoft.CSharp;
using SoftEtherApi.Containers;

namespace SoftEtherApi.Infrastructure
{
    public static class PocoCreator
    {
        public static void GenerateSourceFile(string className, SoftEtherParameterCollection list,
            bool containsMany = false)
        {
            var fileBuffer = new StringBuilder();
            fileBuffer.AppendLine(@"using System;");
            fileBuffer.AppendLine();
            fileBuffer.AppendLine(@"namespace SoftEtherApi.SoftEtherModel");
            fileBuffer.AppendLine(@"{");
            fileBuffer.AppendLine($@"    public class {className} : BaseSoftEtherModel<{className}>");
            fileBuffer.AppendLine(@"    {");

            var usingLists = false;
            var compiler = new CSharpCodeProvider();
            foreach (var el in list)
            {
                var fieldName = ModelDeserializer.FilterKeyName(el.Key);
                var fieldType = el.Value[0].GetType();
                var fieldTypeName = compiler.GetTypeOutput(new CodeTypeReference(fieldType));

                if (fieldName.Contains("Time") || fieldName.Contains("Date"))
                    fieldTypeName = "DateTime";

                if (el.Value.Count > 1 && !containsMany)
                {
                    usingLists = true;
                    fieldTypeName = $"List<{fieldTypeName}>";
                }

                fileBuffer.AppendLine($@"        public {fieldTypeName} {fieldName};");
            }

            if (usingLists)
                fileBuffer.Insert(0, $"using System.Collections.Generic;{Environment.NewLine}");

            fileBuffer.AppendLine(@"    }");
            fileBuffer.AppendLine(@"}");
            fileBuffer.AppendLine();

            using (var file = new StreamWriter($"{className}.cs", false))
            {
                file.Write(fileBuffer.ToString());
            }
        }
    }
}