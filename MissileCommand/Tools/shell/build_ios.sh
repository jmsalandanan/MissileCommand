#!/bin/bash -e

export SSHTARGET="ranier@10.3.10.175"
export LOCALDIR="../"
export REMOTEDIR="~/Desktop/UnityBuilds/MissileCommand/"
export BUILDDIR="Builds/"

shell/common_build.sh