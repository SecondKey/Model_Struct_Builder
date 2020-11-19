using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Aga.Diagrams.Tools
{
	/// <summary>
	/// 输入接口（鼠标按下，鼠标移动，鼠标抬起，预定义（Preview）按键按下）
	/// </summary>
	public interface IInputTool
	{
		void OnMouseDown(MouseButtonEventArgs e);
		void OnMouseMove(MouseEventArgs e);
		void OnMouseUp(MouseButtonEventArgs e);

		void OnPreviewKeyDown(KeyEventArgs e);
	}
}
