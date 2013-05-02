#!/bin/bash -e

echo "----------------------------------------
      SSH TARGET:	$SSHTARGET
 LOCAL DIRECTORY:   $LOCALDIR
REMOTE DIRECTORY:   $REMOTEDIR
----------------------------------------"

echo "----------------------------------------
SENDING LOCAL CHANGES TO BUILD MACHINE
----------------------------------------"

rsync -avzr --delete --size-only --no-t --checksum --exclude "Builds" --exclude "Tools" --exclude "Library" --exclude "Temp" $LOCALDIR $SSHTARGET:$REMOTEDIR --exclude "ProjectSettings/ProjectSettings.asset"

echo "----------------------------------------
LOCAL CHANGES SUCCESFULLY UPLOADED
----------------------------------------"

echo "----------------------------------------
BUILDING CURRENT PROJECT. THIS MIGHT TAKE A WHILE...
----------------------------------------"

ssh $SSHTARGET "/Applications/Unity/Unity.app/Contents/MacOS/Unity -projectPath $REMOTEDIR -executeMethod AutoBuilder.PerformiOSBuild -quit"

echo "----------------------------------------
QUITTING ALL XCODE INSTANCES
----------------------------------------"

if killall Xcode 
then
	echo "Closed all Xcode instances"
fi


echo "----------------------------------------
PROJECT BUILD SUCCESSFUL. DOWNLOADING XCODE PROJECT...
----------------------------------------"

if [ ! -d $LOCALDIR$BUILDDIR ]; then
    mkdir -p $LOCALDIR$BUILDDIR
fi;

rsync -avzr --delete --size-only --no-t --checksum $SSHTARGET:$REMOTEDIR$BUILDDIR $LOCALDIR$BUILDDIR

echo "----------------------------------------
XCODE PROJECT LOCATED AT:
$LOCALDIR$BUILDDIR
----------------------------------------"

open $LOCALDIR$BUILDDIR"Unity-iPhone.xcodeproj"
xcodebuild clean -project $LOCALDIR$BUILDDIR"Unity-iPhone.xcodeproj" -target "Unity-iPhone"