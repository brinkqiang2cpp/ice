// **********************************************************************
//
// Copyright (c) 2003-2012 ZeroC, Inc. All rights reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************

#pragma once

#include <IceUtil/Shared.h>

#include <Ice/Handle.h>

namespace IceSSL
{

class TrustManager;
IceUtil::Shared* upCast(IceSSL::TrustManager*);
typedef IceInternal::Handle<TrustManager> TrustManagerPtr;

}
