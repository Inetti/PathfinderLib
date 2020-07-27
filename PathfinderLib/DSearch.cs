﻿using System;
using System.Collections.Generic;

namespace  Pathfinder {    
    public class DSearch<T>  : Pathfinder<T> where T : Node {
        protected PQueue<T> fronter;
        protected Dictionary<T, int> pathcost;
        
        public DSearch() {
            OnInit += Init;
            OnSearch += SearchPath;
        }

        protected void Init(T start) {
            fronter = new PQueue<T>(true);
            fronter.Enqueue(start, 0);
            pathcost = new Dictionary<T, int>();
            pathcost[start] = 0;
        }

        void SearchPath(T end) {
            while (fronter.Count > 0) {
                T root = fronter.Dequeue();
                if (root == end) {
                    break;
                }
                foreach (T n in root.Neighbors) {
                    if (n.IsWalkable()) {
                        int cost = pathcost[root] + n.Cost;
                        if (!pathcost.ContainsKey(n) || cost <  pathcost[n]) {
                            pathcost[n] = cost;
                            fronter.Enqueue(n, cost + end.HeuristicCostTo(n));
                            parents[n] = root;
                        }
                    }
                }
            }
        }
    }
}