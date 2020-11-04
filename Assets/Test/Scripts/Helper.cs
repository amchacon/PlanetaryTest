using UnityEngine;

namespace Test
{
    public static class Helper
    {
        public static Color RandomColor()
	    {
		    return new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
	    }

	    public static Vector2 RandomPositionFromOrigin(Vector2 originPos, float min = 0.5f, float max = 10)
        {
            bool right = Random.value > 0.5f;
            Vector2 pos = new Vector2(originPos.x + Random.Range(min, max), originPos.y + Random.Range(min, max));
            return right ? pos : pos * -1;
	    }
    }
}