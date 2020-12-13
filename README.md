# Xamarin.BlurView

![version](https://img.shields.io/badge/original-v1.6.5-orange.svg?style=flat)
[![NuGet Badge](https://buildstats.info/nuget/Xamarin.BlurView)](https://www.nuget.org/packages/Xamarin.BlurView/)
![Build status](https://yauhenipakala.visualstudio.com/_apis/public/build/definitions/b0170656-dd62-445e-bbb7-d6a336f4a889/1/badge)

Port of [Dimezis/BlurView](https://github.com/Dimezis/BlurView) for Xamarin.Android

---

![Android BlurView Cover](https://github.com/Dimezis/BlurView/blob/master/BlurScreenshot.png)

Dynamic iOS-like blur of underlying Views for Android. 
Includes library and small example project.

BlurView can be used as a regular FrameLayout. It blurs its underlying content and draws it as a background for its children.
BlurView redraws its blurred content when changes in view hierarchy are detected (draw() called). 
It honors its position and size changes, including view animation and property animation.

## [Demo App at Google Play](https://play.google.com/store/apps/details?id=com.eightbitlab.blurview_sample)

## How to use
```XML
<eightbitlab.com.blurview.BlurView
  android:id="@+id/blurView"
  android:layout_width="match_parent"
  android:layout_height="wrap_content"
  app:blurOverlayColor="@color/colorOverlay">

   <!--Any child View here, TabLayout for example-->

</eightbitlab.com.blurview.BlurView>
```

```csharp
float radius = 20;

View decorView = Window.DecorView;
//Activity's root View. Can also be root View of your layout (preferably)
ViewGroup rootView = decorView.FindViewById<ViewGroup>(Android.Resource.Id.content);
//set background, if your root layout doesn't have one
Drawable windowBackground = decorView.Background;

blurView.SetupWith(rootView)
       .SetFrameClearDrawable(windowBackground)
       .SetBlurAlgorithm(new RenderScriptBlur(this))
       .SetBlurRadius(radius)
       .SetHasFixedTransformationMatrix(true);
```

Always try to choose the closest possible root layout to BlurView. This will greatly reduce the amount of work needed for creating View hierarchy snapshot.

You can use `SetHasFixedTransformationMatrix` in case if you are not animating your BlurView, this might slightly improve the performance as BlurView won't have to recalculate its transformation matrix on each frame.

## Supporting API < 17

```
// TODO
```


## Nuget

```
> Install-Package Xamarin.BlurView
```

## Xamarin.Forms

You may use this library for Xamarin.Forms application, for this you need to create custom control with custom renderer. Example of implementation you can see here: https://github.com/wcoder/Xamarin.BlurView/commit/50c71e4bcc58cc07deb108c18dd6a40ccd077832

## Important
BlurView can be used only in a hardware-accelerated window.
Otherwise, blur will not be drawn. It will fallback to a regular FrameLayout drawing process.

## Performance
It takes 1-4ms on Nexus 5 and Nexus 4 to draw BlurView with the setup given in example project.

License
-------

    Copyright 2016 Dmitry Saviuk (Android library), 2017 Yauheni Pakala (Xamarin library)

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.

