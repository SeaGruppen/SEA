using System.IO;

namespace Model.Utilities {
    public class FileIO {

        /// <summary>
        /// Return the platform-specific path of the current project directory
        /// </summary>
        /// <returns></returns>
        public static string? GetProjectPath() {
            // find base path of the project
            string? assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            if (assemblyLocation != null) {
                string? directoryPath = Path.GetDirectoryName(assemblyLocation);
                if (directoryPath != null) {
                    DirectoryInfo? dir = new DirectoryInfo(directoryPath);
                    if (dir != null) {
                        while (dir.Parent != null && dir.Name != "src") {
                            dir = dir.Parent;
                        }
                    }
                    return dir?.FullName;
                }
            }
            return null;
        }
    }
}