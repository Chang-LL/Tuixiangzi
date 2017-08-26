using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGame
{
    public class CommandManager
    {
        Stack<CommandBase> commandStack = new Stack<CommandBase>();
        Stack<CommandBase> redoStack = new Stack<CommandBase>();
        public CommandManager()
        {
            
        }

        public void Clear()
        {
            commandStack.Clear();//清除撤消堆栈
            redoStack.Clear();  //清除重做堆栈
        }

        public void Execute(CommandBase command)
        {
            redoStack.Clear();//清除重做堆栈
            command.Execute();//执行命令
            commandStack.Push(command);//将命令推入栈顶
        }

        public void Redo()
        {
            if (redoStack.Count < 1)//如果重做堆栈为空
            {
                return; //不执行任何重做操作
            }
            CommandBase command = redoStack.Pop();//弹出重做命令
            command.Execute();//执行重做
            commandStack.Push(command);//压入栈顶
        }

        public void Undo()
        {
            if (commandStack.Count < 1)//如果撤消堆栈中没有命令
            {
                return; //无法进行撤消
            }
            CommandBase command = commandStack.Pop();//从栈顶取出命令
            command.Undo();//执行撤消
            redoStack.Push(command);//将撤消压入栈顶
        }
    }
}