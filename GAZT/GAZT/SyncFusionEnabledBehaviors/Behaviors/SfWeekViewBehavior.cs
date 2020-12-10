using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZTeServicesApp.Behaviors
{
    [Preserve(AllMembers = true)]
    public class SfWeekViewBehavior : Behavior<SfCalendar>
    {
            private Syncfusion.SfCalendar.XForms.SfCalendar calendar;
            protected override void OnAttachedTo(SfCalendar calender)
            {
                base.OnAttachedTo(calender);
                calender.SizeChanged += Bindable_SizeChanged;
            this.calendar = calender;// bindable.Content.FindByName<Syncfusion.SfCalendar.XForms.SfCalendar>("calendar");
                this.calendar.InlineViewMode = InlineViewMode.Agenda;
                this.calendar.MonthViewSettings.SelectionShape = SelectionShape.Circle;
                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    if (Device.RuntimePlatform == "Android")
                    {
                        this.calendar.MonthViewSettings.SelectionRadius = 30;
                    }
                    else
                    {
                        this.calendar.MonthViewSettings.SelectionRadius = 20;
                    }
                }
                this.calendar.NumberOfWeeksInView = 1;
            }
            private void Bindable_SizeChanged(object sender, EventArgs e)
            {
            //var height = (sender as SfCalendar).Content.Height;
            //var width = (sender as SfCalendar).Content.Width;
            var height = DeviceDisplay.MainDisplayInfo.Height;
                var width = DeviceDisplay.MainDisplayInfo.Width;
            if (height > width)
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                this.calendar.AgendaViewHeight = height * 0.75;
                            }
                            else
                            {
                                this.calendar.AgendaViewHeight = height * 0.8;
                            }
                            break;
                        case Device.Android:
                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                this.calendar.AgendaViewHeight = height * 0.95;
                            }
                            else
                            {
                                this.calendar.AgendaViewHeight = height * 0.35;
                            }
                            break;
                        case Device.UWP:
                            this.calendar.AgendaViewHeight = height * 0.75;
                            break;
                    }
                }
                else
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                this.calendar.AgendaViewHeight = height * 0.5;
                            }
                            else
                            {
                                this.calendar.AgendaViewHeight = width * 0.5;
                            }
                            break;
                        case Device.Android:
                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                this.calendar.AgendaViewHeight = height * 0.6;
                            }
                            else
                            {
                                this.calendar.AgendaViewHeight = height * 0.25;
                            }
                            break;
                        case Device.UWP:
                            this.calendar.AgendaViewHeight = height * 0.75;
                            break;
                    }
                }
            }
            protected override void OnDetachingFrom(SfCalendar calender)
            {
                base.OnDetachingFrom(calender);
                calendar = null;
            }
        }
    }
