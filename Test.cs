using System.Reflection.Emit;
using UnityEngine;
using XLua;

namespace XLuaTest
{
    [Hotfix]
    public class HotfixMul
    {
        public float Multiple(float a,float b)
        {
            return a / b;
        }
        public float MultipleVector(Vector3 a,Vector3 b)
        {
            return a.x / b.x + a.y / b.y + a.z / b.z;
        }
    }
    public class NoHotfixMul
    {
        public float Multiple(float a,float b)
        {
            return a * b;
        }
        public float MultipleVector(Vector3 a, Vector3 b)
        {
            return a.x * b.x+a.y*b.y+a.z*b.z;
        }
    }
    public class Test : MonoBehaviour
    {

        string s1, s2;
        HotfixMul calc = new HotfixMul();
        LuaEnv luaenv = new LuaEnv();
        // Use this for initialization
        void Start()
        {
            
           
           
            NoHotfixMul ordinaryCalc = new NoHotfixMul();
            Debug.Log("Before Fix: 2 x 2 = " + calc.Multiple(2, 2));
            Debug.Log("Before Fix: Vector3(2, 3, 4) * Vector3(1, 2, 3) = " + calc.MultipleVector(new Vector3(2, 3, 4), new Vector3(1, 2, 3)));
            s1 = "Before Fix: 2 x 2 = " + calc.Multiple(2, 2) + "\n" + "Before Fix: Vector3(2, 3, 4) * Vector3(1, 2, 3) = " + calc.MultipleVector(new Vector3(2, 3, 4), new Vector3(1, 2, 3));
           
           
        }

        // Update is called once per frame
        void Update()
        {
           
        }
        private void OnGUI()
        {
            GUIStyle style = GUI.skin.textArea;
            style.normal.textColor = Color.red;
            style.fontSize = 16;
            if (GUI.Button(new Rect(10, 10, 300, 80), "AfterHotfix"))
            {
                luaenv.DoString(@"
            xlua.hotfix(CS.XLuaTest.HotfixMul, 'Multiple', function(self, a, b)
                
                return a*b
            end)");
                luaenv.DoString(@"
            xlua.hotfix(CS.XLuaTest.HotfixMul, 'MultipleVector', function(self, a, b)
                local Vector3 = CS.UnityEngine.Vector3

                return a.x * b.x+a.y*b.y+a.z*b.z
            end)");
                Debug.Log("After Fix: 2 x 2 = " + calc.Multiple(2, 2));
                Debug.Log("After Fix: Vector3(2, 3, 4) * Vector3(1, 2, 3) = " + calc.MultipleVector(new Vector3(2, 3, 4), new Vector3(1, 2, 3)));
                s1 = "After Fix: 2 x 2 = " + calc.Multiple(2, 2) + "\n" + "After Fix: Vector3(2, 3, 4) * Vector3(1, 2, 3) = " + calc.MultipleVector(new Vector3(2, 3, 4), new Vector3(1, 2, 3));
               
            }
            if (GUI.Button(new Rect(350, 10, 300, 80), "return"))
            {
                luaenv.DoString(@"
            xlua.hotfix(CS.XLuaTest.HotfixMul, 'Multiple', function(self, a, b)
                
                return a/b
            end)");
                luaenv.DoString(@"
            xlua.hotfix(CS.XLuaTest.HotfixMul, 'MultipleVector', function(self, a, b)
                local Vector3 = CS.UnityEngine.Vector3

                return a.x/b.x+a.y/b.y+a.z/b.z
            end)");
                Debug.Log("After Fix: 2 x 2 = " + calc.Multiple(2, 2));
                Debug.Log("After Fix: Vector3(2, 3, 4) * Vector3(1, 2, 3) = " + calc.MultipleVector(new Vector3(2, 3, 4), new Vector3(1, 2, 3)));
                s1 = "return : 2 x 2 = " + calc.Multiple(2, 2) + "\n" + "return : Vector3(2, 3, 4) * Vector3(1, 2, 3) = " + calc.MultipleVector(new Vector3(2, 3, 4), new Vector3(1, 2, 3));

            }
            GUI.TextArea(new Rect(10, 100, 500, 200), s1, style);
           
        }



    }
    }
