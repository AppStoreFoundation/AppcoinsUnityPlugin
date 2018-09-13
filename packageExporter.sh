#packageExporter.sh

if [ -z "$UNITY_PATH" ]; then
    echo "Need to set UNITY_PATH! Please run export UNITY_PATH=/path/to/Unity; export PROJ_PATH=/path/to/Unity/Proj; sh packageExport.sh"
    exit 1
fi  

if [ -z "$PROJ_PATH" ]; then
    echo "Need to set UNITY_PATH! Please run export UNITY_PATH=/path/to/Unity; export PROJ_PATH=/path/to/Unity/Proj; sh packageExport.sh"
    exit 1
fi  

"$UNITY_PATH" -batchmode -projectPath "$PROJ_PATH" -logFile -quit -executeMethod ExportPackageAutomatically.ExportFromUnity