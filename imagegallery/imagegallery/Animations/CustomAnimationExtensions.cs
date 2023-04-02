using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace imagegallery.Animations
{
    public static class CustomAnimationExtensions
    {
        /// <summary>
        /// Shakes the button async.
        /// </summary>
        /// <returns>The Task</returns>
        /// <param name="times">Number of times it goes left AND right (2 = Left-Right-Left-Right).</param>
        /// <param name="distance">Distance in percentage of width (0.2 = 20% of width).</param>
        /// <param name="scale">Scale in percentage.</param>
        /// <param name="scale">Duration of the entire animation in ms (1000 = 1 second).</param>
        public static async Task ShakeAsync(this View view, int times = 2, double distance = 0.1, double scale = 1)
        {
            ViewExtensions.CancelAnimations(view);

            uint stepDuration = 100;

            for (var i = 0; i < times; i++)
            {
                await view.TranslateTo(distance * view.Width, 0, stepDuration);
                await view.TranslateTo(-distance * view.Width, 0, stepDuration);
            }

            await Task.WhenAll(
                view.ScaleTo(1, stepDuration / 2),
                view.TranslateTo(0, 0, stepDuration / 2));
        }

        /// <summary>
        /// Animates the button down.
        /// </summary>
        /// <returns>The Task</returns>
        /// <param name="duration">Duration of the animation in ms (1000 = 1 second).</param>
        /// <param name="scaleTo">Scale to apply in percentage (0.2 = 20%).</param>
        /// <param name="fadeTo">Fade to apply in percentage (0.2 = 20%).</param>
        public static Task AnimatePressedAsync(this View view, uint duration = 80, double scaleTo = 0.9, double fadeTo = 0.9)
        {
            return Task.WhenAll(
                view.ScaleTo(scaleTo, duration),
                view.FadeTo(fadeTo, duration));
        }

        /// <summary>
        /// Animates the button back.
        /// </summary>
        /// <returns>The Task</returns>
        /// <param name="duration">Duration of the animation in ms (1000 = 1 second).</param>
        /// <param name="scaleTo">Scale to apply in percentage (0.2 = 20%).</param>
        /// <param name="fadeTo">Fade to apply in percentage (0.2 = 20%).</param>
        public static Task AnimateReleasedAsync(this View view, uint duration = 80, double scaleTo = 1, double fadeTo = 1)
        {
            return Task.WhenAll(
                view.ScaleTo(scaleTo, duration),
                view.FadeTo(fadeTo, duration));
        }


        public static Task<Tuple<double, bool>> CommitAsync(this Animation animation, IAnimatable owner, string name, uint rate = 16u, uint length = 250u, Easing easing = null, Func<bool> repeat = null)
        {
            var tcs = new TaskCompletionSource<Tuple<double, bool>>();
            var tt = new Tuple<double, bool>(0, true);
            animation.Commit(owner, name, rate, length, easing, (d, b) =>
            {
                tcs.TrySetResult(new Tuple<double, bool>(d, b));
            }, repeat);
            return tcs.Task;
        }

        public static Task<bool> ColorTo(this VisualElement self, Color fromColor, Color toColor, Action<Color> callback, uint length = 250, Easing easing = null)
        {
            Func<double, Color> transform = (t) =>
              Color.FromRgba(fromColor.R + t * (toColor.R - fromColor.R),
                             fromColor.G + t * (toColor.G - fromColor.G),
                             fromColor.B + t * (toColor.B - fromColor.B),
                             fromColor.A + t * (toColor.A - fromColor.A));

            return ColorAnimation(self, "ColorTo", transform, callback, length, easing);
        }

        public static void CancelAnimation(this VisualElement self)
        {
            self.AbortAnimation("ColorTo");
        }

        static Task<bool> ColorAnimation(VisualElement element, string name, Func<double, Color> transform, Action<Color> callback, uint length, Easing easing)
        {
            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            element.Animate<Color>(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }

        public static T GetResource<T>(this ResourceDictionary rd, string key, T defaultValue, bool recursively = true)
        {
            var output = defaultValue;
            if (rd.ContainsKey(key))
                if (rd[key] is T res)
                    output = res;

            if (recursively)
                foreach (ResourceDictionary dic in rd.MergedDictionaries)
                    if (dic.ContainsKey(key))
                        if (dic[key] is T res)
                            output = res;

            return output;
        }

    }
}