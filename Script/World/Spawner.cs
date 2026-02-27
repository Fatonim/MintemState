using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject[] objects2;
    public const int SizeM = 300;
    private int X, Y;
    private Transform player;
    private Biome[,] Map = new Biome[SizeM, SizeM];
    private int[,] FMap = new int[SizeM, SizeM];
    private int[,] Map2 = new int[SizeM, SizeM];



    private float temp_scale = 70f;
    private float humidity_scale = 70f;
    private float height_max = 255;
    private float temp_max = 100;
    private float zoom = 50f;
    public float seed;
    private float seed1, seed2, seed3;

    public class Range
    {
        public float min;
        public float max;

        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
        public bool fits(float value)
        {
            return min <= value && value <= max;
        }
    };

    public class Biome
    {
        public string name;
        public Range heightRange;
        public Range tempRange;
        public Range humidityRange;
        public int numBlock;

        public Biome(string name, Range heightRange, Range tempRange, Range humidityRange, int numBlock)
        {
            this.name = name;
            this.heightRange = heightRange;
            this.tempRange = tempRange;
            this.humidityRange = humidityRange;
            this.numBlock = numBlock;
        }
    };

    public Biome[] biomes = new Biome[]
    {
      new Biome("OceanIce", new Range(0, 70), new Range(-100, 0), new Range(0, 1), 3),
      new Biome("IslandGrass", new Range(0, 30), new Range(20, 60), new Range(0.2f, 0.5f), 0),
      new Biome("IslandSand", new Range(0, 40), new Range(30, 100), new Range(0, 0.5f), 10),
      new Biome("Ocean", new Range(0, 80), new Range(-100, 100), new Range(0, 1), 2),
      new Biome("Lake", new Range(81, 255), new Range(0, 60), new Range(0.85f, 1), 2),
      new Biome("Bitch", new Range(81, 95), new Range(40, 70), new Range(0.2f, 0.8f), 10),
      new Biome("Taiga", new Range(115, 190), new Range(-100, 20), new Range(0f, 0.5f), 7),
      new Biome("Desert", new Range(110, 190), new Range(50, 100), new Range(0, 0.6f), 10),
      new Biome("Ground", new Range(81, 190), new Range(21, 70), new Range(0.2f, 0.85f), 0),
      new Biome("StoneMountains", new Range(191, 255), new Range(-100, 50), new Range(0, 0.5f), 9)
    };

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player.position = new Vector3(SizeM / 2, SizeM / 2, 0);
        seed = Random.Range(0, 9999999) + 1;
        seed = 8646576;
        seed1 = (int)(seed / 13);
        seed2 = (int)(seed * 3);
        seed3 = (int)(seed / 7);
        MapGenerator();
    }

    private void MapGenerator()
    {
        for (int i = 0; i < SizeM; i++)
        {
            for (int j = 0; j < SizeM; j++)
            {
                int Distance;
                float temp = 0, humidity = 0;
                float value = Mathf.PerlinNoise((i + seed1) / zoom, (j + seed1) / zoom);
                int h = (int)(value * height_max);
                if (h > 255) h = 255;
                if (h < 0) h = 0;

                float temperatureNoise = Mathf.PerlinNoise((i + seed2) / temp_scale, (j + seed2) / temp_scale);
                if (h > 81) temp = temperatureNoise * temp_max - (h - 80) * 0.45f;
                else temp = temperatureNoise * temp_max - 25;

                float humidityNoise = Mathf.PerlinNoise((i + seed3) / humidity_scale, (j + seed3) / humidity_scale);
                if (humidityNoise > 1) humidityNoise = 1;
                if (humidityNoise < 0) humidityNoise = 0;
                humidity = humidityNoise;


                for (int k = 0; k < biomes.Length; k++)
                {
                    if (biomes[k].heightRange.fits(h))
                    {
                        if (biomes[k].tempRange.fits(temp))
                        {
                            if (biomes[k].humidityRange.fits(humidity))
                            {
                                Map[i, j] = new Biome(biomes[k].name, biomes[k].heightRange, biomes[k].tempRange, biomes[k].humidityRange, biomes[k].numBlock);


                                if (Map[i, j].numBlock == 0) //grass
                                {
                                    Distance = (int)((0.852f - humidity) * 70);
                                    if (Random.Range(0, 5) == 0) Map[i, j].numBlock = 1;
                                    if (Random.Range(0, Distance) == 0) Map2[i, j] = 3;
                                    else if (Random.Range(0, 40) == 0) Map2[i, j] = 2;
                                } else if (Map[i, j].numBlock == 10) //sand
                                {
                                    if (i > 1)
                                        if (Random.Range(0, 25) == 0 && Map2[i-1, j] == 0 && Map2[i - 2, j] == 0) Map2[i, j] = 1;
                                } else if (Map[i, j].numBlock == 9) //stone
                                {
                                    if (Random.Range(0, 3) == 0) Map[i, j].numBlock = 8;
                                } else if (Map[i, j].name == "Taiga") //dirt
                                {
                                    Distance = (int)((0.6f - humidity) * 50);
                                    if (Random.Range(0, Distance) == 0) Map2[i, j] = 4;
                                    if (Random.Range(0, 3) == 0) Map[i, j].numBlock = 6;
                                }


                                //snow
                                if (humidity <= 0.4f && temp < -5)
                                {
                                    Distance = Mathf.Abs(16 + (int)temp);
                                    if (temp < -20) Distance = 2;
                                    if (Random.Range(0, Distance) == 0)
                                    {
                                        if (Random.Range(0, 3) == 0) Map[i, j].numBlock = 4;
                                        else Map[i, j].numBlock = 5;
                                    }
                                }


                                /*if (j % 2 == 0)
                                {
                                    Instantiate(objects[Map[i, j].numBlock], new Vector3(1.5f * j, 1.75f * i), Quaternion.identity);
                                }
                                else
                                {
                                    Instantiate(objects[Map[i, j].numBlock], new Vector3(1.5f * j, 1.75f * i + 0.875f), Quaternion.identity);
                                }
                                if (Map2[i, j] != 0)
                                {
                                    if (j % 2 == 0)
                                        Instantiate(objects2[Map2[i, j]], new Vector3(1.5f * j, 1.75f * i), Quaternion.identity);
                                    else
                                        Instantiate(objects2[Map2[i, j]], new Vector3(1.5f * j, 1.75f * i + 0.875f), Quaternion.identity);
                                }*/
                                break;
                            }
                        }
                    }
                    if (Map[i, j] == null) Map[i, j] = new Biome(biomes[8].name, biomes[8].heightRange, biomes[8].tempRange, biomes[8].humidityRange, biomes[8].numBlock);
                }
                if (j % 2 == 0)
                {
                    int y = (int)(1.75f * i);
                    if (y % 325 == 0 || y % 326 == 0 || y % 327 == 0 || y % 328 == 0) Map2[i, j] = 0;
                }
                else
                {
                    int y = (int)(1.75f * i + 0.875f);
                    if (y % 325 == 0 || y % 326 == 0 || y % 327 == 0 || y % 328 == 0) Map2[i, j] = 0;
                }
            }
        }
    }


    private void Update()
    {
        X = (int)(player.position.x / 1.5f);
        Y = (int)(player.position.y / 1.75f);
        if (Y > 2 && Y < SizeM-4 && X > 6 && X < SizeM-7)
        {
            for (int i = Y - 3; i < Y + 4; i++)
            {
                for (int j = X - 4; j < X + 6; j++)
                {
                    if (FMap[i, j] == 0)
                    {
                        if (j % 2 == 0)
                            Instantiate(objects[Map[i, j].numBlock], new Vector3(1.5f * j, 1.75f * i), Quaternion.identity);
                        else
                            Instantiate(objects[Map[i, j].numBlock], new Vector3(1.5f * j, 1.75f * i + 0.875f), Quaternion.identity);
                        if (Map2[i, j] != 0)
                        {
                            if (j % 2 == 0)
                                Instantiate(objects2[Map2[i, j]], new Vector3(1.5f * j, 1.75f * i), Quaternion.identity);
                            else
                                Instantiate(objects2[Map2[i, j]], new Vector3(1.5f * j, 1.75f * i + 0.875f), Quaternion.identity);
                        }
                        FMap[i, j] = 1;
                    }
                }
            }
        }
    }
}