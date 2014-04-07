#Animation Manager for Xaml#

![](https://github.com/brainoffline/AnimationManager/blob/master/Images/AnimatedHead.gif?raw=true)

**Animation Manager** is designed to animate xaml controls in a very very easy way.  These animations work the same way on both Windows 8 as well as Windows Phone 8.

There are a number of *pain* points when animating Xaml controls.  


- If it ain't easy, it ain't gonna get done
- It is more often just left out as not seen as unimportant
- There is no design language for animation (Windows 8 has made a good start).  You are most often given images straight out of Photoshop
- Animations can start behind the splash screen and are often missed.
- MVVM implementations often complicate the separation of view and model.  View models make animation difficult.  MVCB may be a better approach.

**Animation Manager** attempts to address some of these issues.

- Simple is best, but drop down to the next level to do something more powerful


## Three Levels ##
There are three different approaches to using the Animation Manager.

- Declarative in XAML (Triggered animations)
- Call built in animations from code
- Define your own animations

## Declarative Animation in XAML ##
The simplest way to animate xaml elements is to declare it in the xaml. Just declare the animation trigger and which animation you want to do.
There are three triggers for declaring animations

- When a page loads
- When a page closes
- When idling

Here is an example of animating a button

	<Button Content="Hello, World" >
    	<bam:AnimationTrigger.Open>
    	    <bam:BounceInUpAnimation Delay="0.3"/>
	    </bam:AnimationTrigger.Open>
    	<bam:AnimationTrigger.Idle>
    	    <bam:PulseAnimation Duration="4.0"/>
	    </bam:AnimationTrigger.Idle>
    	<bam:AnimationTrigger.Close>
    	    <bam:BounceOutUpAnimation/>
	    </bam:AnimationTrigger.Close>
	</Button>


The default animations are simple and quick and quickly add to the polish of an app.
For the above button, it will bounce in when the screen is loaded, slowly pulsate while the screen is displayed, then bounce out as the screen closes.

### Animate ListBox Items ###

Here is an example of animating a ListBox. The cool thing here is you can use ANY predefined animation to animate items in. Or if you are inspired, you can define your own animation.

	<Page.Resources>
		<bam:FadeInUpAnimation x:Key="ListBoxAnimation1" Delay="0.03" Duration="0.3" Distance="150" />
	</Page.Resources>

	<ListBox ItemsSource="{Binding ExperimentTitleList}" 
    	bam:ListAnimation.LoadItem="{StaticResource ListBoxAnimation1}" 
	    bam:ListAnimation.LoadItemDelay="0.1" />


### Splash Screen Aware ###
Animations do not start until the splash screen has been dismissed.

### Navigation ###
*Navigating* in and out of pages can be a pain if you want to make it pretty. You want your animations to finish before navigating away from current screen.  Calling the following will call all the AnimationTrigger.Close animations.

	await AnimationTrigger.AnimateClose()
 
This is not yet automatic and planned for a future release.

### Page Transitions ###
Lots of cool page transitions.


## Animations from Code ##
*async* rocks for animation.  This next bit of code awaits for all animations to finish, before navigating to the next screen.

	private async void HelloButton_OnClick(object sender, RoutedEventArgs e)
	{
	    await Task.WhenAll(
	        AnimationTrigger.AnimateClose(),
	        HelloButton.AnimateAsync(new FlipAnimation()),
	        HelloButton.AnimateAsync(new BounceOutDownAnimation()),
	        SponsorText.AnimateAsync(new LightSpeedOutLeftAnimation())
	    );
	
	    NavigationService.Navigate(new Uri("/MenuPage.xaml", UriKind.Relative));
	}



## Full Control over animations ##
For those control freaks out there, there is also great control over animations

	var sb = new Storyboard();
	var a1 = grid.AnimateProperty<DoubleAnimationUsingKeyFrames>(AnimationProperty.TranslateX)
	         .AddEasingKeyFrame(0, 0)
	         .AddEasingKeyFrame(0.3, -90)
	         .AddEasingKeyFrame(0.6, 0, new CubicEase { EasingMode = EasingMode.EaseOut});
	sb.Children.Add(a1);
	sb.Completed += (s, a) => { };
	sb.Begin();


## Animation Awesomeness

Just a few more features thrown in.

#### Animated Spritesheets 

Animation Manager includes an **AnimatedImage** control so your can use SpriteSheets to animate images.

#### Animated TextBlock

Animate individual characters in the **AnimatingTextBlock** control.  Create some awesome effects.

#### Animation Manager

Register animations with the **AnimationManager** control.  This will allow you to Pause or stop any registered storyboards.  Very useful when you have lots going on in a screen.

#### Pan Control

A Panning Background.  Create a *parallax* effect by having multiple panning backgrounds.

 