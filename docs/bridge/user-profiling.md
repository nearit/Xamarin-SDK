# User profiling & Opt-Out (Bridge)

NearIT creates an anonymous profile for every user of your app. You can choose to add data to user profile. This data will be available inside recipes to allow the creation of user targets.

## Add user-data to NearIT

You can set user data with this method, it can be called multiple times to set several user data:
```
NearPCL.SetUserData(key, value);
```

**Remember** <br>
You will need to use the "**Settings> Data Mapping**" section of [NearIT](https://go.nearit.com) to configure the data fields to be used inside recipes.



## Save the profile ID!

If you can, we recommend you to store the NearIT profileID in your CRM database for two main reasons:

- it allows you to link our analytics to your users
- it allows to associate all the devices of an user to the same NearIT profile.


Getting the local profile ID of an user is easy:
```
NearPCL.GetProfileId(
        (profileId) => {
            // handle the profileId
        },
        (error) => {
            // handle the error
        });
```


If you detect that your user already has a NearIT profileID in your CRM database (i.e. after a login), you should manually write it on a local app installation:
```
NearPCL.SetProfileId(profile);
```


Whenever a users **signs out** from your app, you should reset the NearIT profileID:
```
NearPCL.ResetProfileId(
        (profileId) => {
            // handle the profileId
        },
        (error) => {
            // handle the error
        });
```

## Opt-Out

You can **opt-out** a profile and its device:
```
NearPCL.OptOut(
                (success) => {
                // opt out successfully (the argument value is not important)
                },
                (error) => {
                // deal with the error
                });
```
If the opt-out call is successful all the **user-data** and **trackings** will be deleted and the **SDK will cease to work** (the user's devices will not receive further notifications).
