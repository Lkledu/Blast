mergeInto(LibraryManager.library, {

	Ext_SendMsg: function(controllerId, msg, data){
		window.tiltspotJSInterface.send.msg(controllerId, Pointer_stringify(msg), Pointer_stringify(data));
	},

	Ext_SendSetOrientation: function(controllerId, orientation){
		window.tiltspotJSInterface.send.setOrientation(controllerId, orientation);
	},

	Ext_SendVibrate: function(controllerId, duration){
		window.tiltspotJSInterface.send.vibrate(controllerId, duration);
	},

	Ext_BroadcastMsg: function(msg, data){
		window.tiltspotJSInterface.broadcast.msg(Pointer_stringify(msg), Pointer_stringify(data));
	},

	Ext_BroadcastSetOrientation: function(orientation){
		window.tiltspotJSInterface.broadcast.setOrientation(orientation);
	},

	Ext_BroadcastVibrate: function(duration){
		window.tiltspotJSInterface.broadcast.vibrate(duration);
	},

	Ext_GetAssetUrl: function(filename){
        var res = window.tiltspotJSInterface.get.assetUrl(Pointer_stringify(filename));
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetIsControllerConnected: function(controllerId){
        var res = window.tiltspotJSInterface.get.isControllerConnected(controllerId);
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetControllerLatency: function(controllerId){
        var res = window.tiltspotJSInterface.get.controllerLatency(controllerId);
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetUsers: function(){
        var res = window.tiltspotJSInterface.get.users();
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetUser: function(controllerId){
        var res = window.tiltspotJSInterface.get.user(controllerId);
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetUserId: function(controllerId){
        var res = window.tiltspotJSInterface.get.userId(controllerId);
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

	Ext_GetNumberOfUsers: function(controllerId){
        var res = window.tiltspotJSInterface.get.numberOfUsers();
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetUserProfilePicture: function(controllerId){
        var res = window.tiltspotJSInterface.get.userProfilePicture(controllerId);
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetUserNickname: function(controllerId){
        var res = window.tiltspotJSInterface.get.userNickname(controllerId);
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetHostId: function(){
        var res = window.tiltspotJSInterface.get.hostId();
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetHost: function(){
        var res = window.tiltspotJSInterface.get.host();
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetIsUserHost: function(controllerId){
        var res = window.tiltspotJSInterface.get.isUserHost(controllerId);
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetEntryCode: function(){
        var res = window.tiltspotJSInterface.get.entryCode();
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetIsGameOwnedByUser: function(userId){
        var res = window.tiltspotJSInterface.get.isGameOwnedByUser(userId);
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetBrowserLanguage: function(){
        var res = window.tiltspotJSInterface.get.browserLanguage();
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GetStartTime: function(){
        var res = window.tiltspotJSInterface.get.startTime();
        var bufferSize = lengthBytesUTF8(res) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(res, buffer, bufferSize);
        return buffer;
    },

    Ext_GameStarted: function(){
        window.tiltspotJSInterface.gameStarted();
    },

});