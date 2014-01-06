﻿//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by a resource generator.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using Windows.ApplicationModel.Resources;

namespace XboxMediaRemote.App.Resources 
{
    public static class Strings 
	{
        public static ResourceLoader ResourceLoader 
		{ 
			get; private set; 
		}

        static Strings() 
		{
            ResourceLoader = new ResourceLoader();
        }

		public static string Get(string resource)
		{
			return ResourceLoader.GetString(resource); 
		}

		public static string Format(string resource, params object[] args)
		{
			return String.Format(ResourceLoader.GetString(resource), args);
		}

        public static string BrowseFolderEmptyText 
		{
            get 
			{ 
				return ResourceLoader.GetString("BrowseFolderEmpty/Text"); 
			}
        }

        public static string GroupsNumberTitle 
		{
            get 
			{ 
				return ResourceLoader.GetString("GroupsNumberTitle"); 
			}
        }

        public static string GroupsSymbolTitle 
		{
            get 
			{ 
				return ResourceLoader.GetString("GroupsSymbolTitle"); 
			}
        }

        public static string MediaHubTitleText 
		{
            get 
			{ 
				return ResourceLoader.GetString("MediaHubTitle/Text"); 
			}
        }

        public static string MediaSearchPlaceholderText 
		{
            get 
			{ 
				return ResourceLoader.GetString("MediaSearch/PlaceholderText"); 
			}
        }

        public static string MediaTypeImage 
		{
            get 
			{ 
				return ResourceLoader.GetString("MediaTypeImage"); 
			}
        }

        public static string MediaTypeVideo 
		{
            get 
			{ 
				return ResourceLoader.GetString("MediaTypeVideo"); 
			}
        }

        public static string SearchResultsHeaderText 
		{
            get 
			{ 
				return ResourceLoader.GetString("SearchResultsHeader/Text"); 
			}
        }

        public static string SearchResultsNoneText 
		{
            get 
			{ 
				return ResourceLoader.GetString("SearchResultsNone/Text"); 
			}
        }

        public static string WelcomeAppText 
		{
            get 
			{ 
				return ResourceLoader.GetString("WelcomeApp/Text"); 
			}
        }

        public static string WelcomeSubheaderText 
		{
            get 
			{ 
				return ResourceLoader.GetString("WelcomeSubheader/Text"); 
			}
        }

        public static string WelcomeTitleText 
		{
            get 
			{ 
				return ResourceLoader.GetString("WelcomeTitle/Text"); 
			}
        }

    }
}
