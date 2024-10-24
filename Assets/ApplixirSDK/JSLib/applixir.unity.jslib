var ApplixirPlugin = {

    $impl: {},
    local_options: {},

    adStatusCallbackX: function (status) {
        if (this.local_options.verbosity > 2) {
            console.log('[ApplixirWebGL] Ad Status:', status);
        }
        var iresult = 0;
        switch (status) {
            case "ad-watched":
                iresult = 1;
                break;
            case "network-error":
                iresult = 2;
                break;
            case "ad-blocker":
                iresult = 3;
                break;
            case "ad-interrupted":
                iresult = 4;
                break;
            case "ads-unavailable":
                iresult = 5;
                break;
            case "cors-error":
                iresult = 6;
                break;
            case "no-zoneId":
                iresult = 7;
                break;
            case "ad-started":
                iresult = 8;
                break;
            case "sys-closing":
                iresult = 9;
                break;
            case "ad-rewarded":
                iresult = 10;
                break;
            case "ad-ready":
                iresult = 11;
                break;
            case "ad-rejected":
                iresult = 12;
                break;
            case "ad-maximum":
                iresult = 13;
                break;
            case "ad-violation":
                iresult = 14;
                break;
            default:
                iresult = 0;
                break;
        }
        Runtime.dynCall('vi', window.applixirCallback, [iresult]);
        if (this.local_options.verbosity > 2) {
            console.log('[ApplixirWebGL] Ad Status done:', status);
        }
    },

    ShowVideo__deps: [
        '$impl',
        'adStatusCallbackX'
    ],

    ShowVideo: function (zone, accountId, site, user, callback, verbo) {
        this.local_options = {
            zoneId: zone,           // the zone ID from the "Sites" page
            accountId: accountId,   // your account ID from the "Account" or "Site" page
            siteId: site,           // the ID for this site from the "Sites" page in your applixir.com account
            userId: user,           // required by ad industry. Valid UUID for current user, UUID4 recommended but no PII values
            adStatusCb: callback,   // local callback reference
            verbosity: verbo        // 0..5 - for debugging only, leave commented out or set to zero for production use
        };
        if (this.local_options.verbosity > 2) {
            console.log(this.local_options);
        }
        window.applixirCallback = callback;
        invokeApplixirVideoUnit(this.local_options);
    }
};

autoAddDeps(ApplixirPlugin, '$impl');
mergeInto(LibraryManager.library, ApplixirPlugin);