using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CollaborationBoard.Models;
using System.Collections;

namespace CollaborationBoard
{
    public class DrawCommannd:ICommand
    {
        private Stack<MetaData> _stackSketchHistory;
        public MetaData sketch { get; set; }

        //Constructor
        public DrawCommannd(Stack<MetaData> dropOutSketch)
        {
            _stackSketchHistory = dropOutSketch;
        } 

        public void Do()
        {
            _stackSketchHistory.Push(sketch);
        }

        public IEnumerable UnDo()
        {
            DrawState currSketchDrawState;
            IList<MetaData> undoList = new List<MetaData>();
            try
            {
                do
                {
                    MetaData popedSketch = _stackSketchHistory.Pop();
                    if (popedSketch != null)
                    {
                        currSketchDrawState = popedSketch.DrawState;
                        popedSketch.Color = "white";
                        //change down to move for erasing purpose
                        popedSketch.DrawState = popedSketch.DrawState == DrawState.down ? DrawState.move : popedSketch.DrawState;
                        popedSketch.Width++;
                        undoList.Add(popedSketch);
                    }
                    else
                    {
                        break;
                    }

                } while (currSketchDrawState != DrawState.down);
            }
            catch(Exception ee)
            {
                throw ee;
            }
            return undoList.ToList();
        }
    }
}