// **********************************************************************
//
// Copyright (c) 2003-2012 ZeroC, Inc. All rights reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************

#pragma once

#include <Ice/TransceiverF.h>
#include <Ice/InstanceF.h>
#include <Ice/TraceLevelsF.h>
#include <Ice/LoggerF.h>
#include <Ice/Connector.h>
#include <Ice/Network.h>

namespace IceInternal
{

class StreamConnector : public Connector
{
public:
    
    virtual TransceiverPtr connect();

    virtual Ice::Short type() const;
    virtual std::string toString() const;

    virtual bool operator==(const Connector&) const;
    virtual bool operator!=(const Connector&) const;
    virtual bool operator<(const Connector&) const;

private:

    StreamConnector(const InstancePtr&, Ice::Short, const Address&, Ice::Int, const Ice::ProtocolVersion&,
                    const Ice::EncodingVersion&, const std::string&); 
    virtual ~StreamConnector();
    friend class StreamEndpointI;

    const InstancePtr _instance;
    const Ice::Short _type;
    const TraceLevelsPtr _traceLevels;
    const ::Ice::LoggerPtr _logger;
    const Address _addr;
    const Ice::Int _timeout;
    const Ice::ProtocolVersion _protocol;
    const Ice::EncodingVersion _encoding;
    const std::string _connectionId;
};

}
