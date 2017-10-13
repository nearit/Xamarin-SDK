# User Profiling

NearIT creates an anonymous profile for every user of your app. You can choose to add data to user profile. This data will be available inside recipes to allow the creation of user targets.

## Send User-Data to NearIT

We automatically create an anonymous profile for every installation of the app. You can check that a profile was created by checking the existance of a profile ID.
```csharp
String profileId = NearItManager.Instance.ProfileId;
```
If the result is null, it means that no profile is associated with the app installation (probably due to a network error). The SDK will re-try to create a profile at every start, and every time a new user data is set.

After the profile is created set user data:
```csharp
NearItManager.Instance.SetUserData("name", "peter", _successListener);
```

If you have multiple data properties, set them in batch:
```csharp
var userData = new Dictionary<string, string> {
  { "name", "John" } , {"age", "23"} , { "saw_tutorial" , "true" }
};
NearItManager.Instance.SetBatchUserData(userData, _successListener);
```
If you try to set user data before creating a profile the error callback will be called.

If you want to reset your profile use this method:
```csharp
NearItManager.Instance.ResetProfileId();
```
Further calls to *ProfileId* will return null. A creation of a new profile after the reset will create a profile with no user data.
<br>
**Remember** <br>
You will need to use the "**Settings> Data Mapping**" section of [NearIT](https://go.nearit.com) to configure the data fields to be used inside recipes.

## Save the profile ID!

If you can, we recommend you to store the NearIT profileID in your CRM database for two main reasons:

- it allows you to link our analytics to your users
- it allows to associate all the devices of an user to the same NearIT profile.


Getting the local profile ID of an user is easy:
```java
String profileId = NearItManager.Instance.ProfileId;
```


If you detect that your user already has a NearIT profileID in your CRM database (i.e. after a login), you should manually write it on a local app installation:
```java
NearItManager.Instance.ProfileId = "FROM_SERVER";
```
