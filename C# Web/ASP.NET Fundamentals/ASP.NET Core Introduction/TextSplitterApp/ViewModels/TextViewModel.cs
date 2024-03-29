﻿using System.ComponentModel.DataAnnotations;

namespace TextSplitterApp.ViewModels
{
	public class TextViewModel
	{
		[Required]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "The field Text must be a string with a minimum length of 2 and\r\nmaximum length of 30.")]
		public string Text { get; set; } = null!;

		public string? SplitText { get; set;}
	}
}
