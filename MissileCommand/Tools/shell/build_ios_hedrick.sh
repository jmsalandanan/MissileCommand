#!/bin/bash -e

export SSHTARGET="ranier@10.3.10.175"
export LOCALDIR="../"
export REMOTEDIR="~/Desktop/UnityBuilds/lrrh_hedrick/"
export BUILDDIR="Builds/iOS/"

shell/common_build.sh