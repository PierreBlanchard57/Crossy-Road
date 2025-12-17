using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad.chunks
{
    public class ProbabilityStack
    {
        private int maxValue=0;
        private Dictionary<Chunk, int[]> chunks=new Dictionary<Chunk, int[]>();
        public ProbabilityStack()
        {

        }
        public void addChunk(Chunk chunk)
        {
            chunks.Add(chunk, new int[] { maxValue,chunk.Weight+maxValue });
            maxValue += chunk.Weight;
        }
        public Chunk pickRandomChunk()
        {
            int randomValue=new Random().Next(0,maxValue);
            Chunk choosen=null;
            foreach (var chunk in chunks)
            {
                if (chunk.Value[0]<randomValue && chunk.Value[1] >= randomValue)
                {
                    choosen = chunk.Key;
                }
            }
            return choosen;
        }
    }
}
