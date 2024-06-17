using System;

public class Engine
{
    private int[,] resenje;
    private int[,] prikaz;
    private static Random slucajniBroj = new Random();

    public Engine()
    {
        resenje = new int[9, 9];
        prikaz = new int[9, 9];
        GenerisiPrikaz();
    }

    public int[,] VratiPrikaz()
    {
        return prikaz;
    }

    public int[,] VratiResenje()
    {
        return resenje;
    }

    public void GenerisiPrikaz()
    {
        GenerisiResenje();
        KreirajPrikaz();
    }

    private void GenerisiResenje()
    {
        int[,] osnovnoResenje = {
        { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
        { 4, 5, 6, 7, 8, 9, 1, 2, 3 },
        { 7, 8, 9, 1, 2, 3, 4, 5, 6 },
        { 2, 3, 4, 5, 6, 7, 8, 9, 1 },
        { 5, 6, 7, 8, 9, 1, 2, 3, 4 },
        { 8, 9, 1, 2, 3, 4, 5, 6, 7 },
        { 3, 4, 5, 6, 7, 8, 9, 1, 2 },
        { 6, 7, 8, 9, 1, 2, 3, 4, 5 },
        { 9, 1, 2, 3, 4, 5, 6, 7, 8 }
    };

        Array.Copy(osnovnoResenje, resenje, osnovnoResenje.Length);
        for (int i = 0; i < 9; i += 3)
        {
            PomesajRedove(i);
        }
        for (int i = 0; i < 9; i += 3)
        {
            PomesajKolone(i);
        }
    }

    private void PomesajRedove(int pocetniRed)
    {
        for (int i = 0; i < 3; i++)
        {
            int red1 = pocetniRed + i;
            int red2 = pocetniRed + slucajniBroj.Next(3);
            ZameniRedove(red1, red2);
        }
    }

    private void PomesajKolone(int pocetnaKolona)
    {
        for (int i = 0; i < 3; i++)
        {
            int kol1 = pocetnaKolona + i;
            int kol2 = pocetnaKolona + slucajniBroj.Next(3);
            ZameniKolone(kol1, kol2);
        }
    }

    private void ZameniRedove(int red1, int red2)
    {
        if (red1 == red2) return;
        for (int kolona = 0; kolona < 9; kolona++)
        {
            int temp = resenje[red1, kolona];
            resenje[red1, kolona] = resenje[red2, kolona];
            resenje[red2, kolona] = temp;
        }
    }

    private void ZameniKolone(int kol1, int kol2)
    {
        if (kol1 == kol2) return;
        for (int red = 0; red < 9; red++)
        {
            int temp = resenje[red, kol1];
            resenje[red, kol1] = resenje[red, kol2];
            resenje[red, kol2] = temp;
        }
    }

    private void KreirajPrikaz()
    {
        Array.Copy(resenje, prikaz, resenje.Length);
        for (int i = 0; i < 40; i++)
        {
            int red = slucajniBroj.Next(9);
            int kolona = slucajniBroj.Next(9);
            prikaz[red, kolona] = 0;
        }
    }

    public void OtkrijPolje()
    {
        while (true)
        {
            int red = slucajniBroj.Next(9);
            int kolona = slucajniBroj.Next(9);
            if (prikaz[red, kolona] == 0)
            {
                prikaz[red, kolona] = resenje[red, kolona];
                break;
            }
        }
    }
    public void UmetniVrednost(int red, int kolona, int vrednost)
    {
        prikaz[red, kolona] = vrednost;
    }

    private bool ProveriRed(int red)
    {
        bool[] brojevi = new bool[9];
        for (int kolona = 0; kolona < 9; kolona++)
        {
            int vrednost = prikaz[red, kolona];
            if (vrednost == 0 || brojevi[vrednost - 1])
            {
                return false;
            }
            brojevi[vrednost - 1] = true;
        }
        return true;
    }

    private bool ProveriKolonu(int kolona)
    {
        bool[] brojevi = new bool[9];
        for (int red = 0; red < 9; red++)
        {
            int vrednost = prikaz[red, kolona];
            if (vrednost == 0 || brojevi[vrednost - 1])
            {
                return false;
            }
            brojevi[vrednost - 1] = true;
        }
        return true;
    }

    private bool ProveriKvadrant(int pocetniRed, int pocetnaKolona)
    {
        bool[] brojevi = new bool[9];
        for (int red = 0; red < 3; red++)
        {
            for (int kolona = 0; kolona < 3; kolona++)
            {
                int vrednost = prikaz[pocetniRed + red, pocetnaKolona + kolona];
                if (vrednost == 0 || brojevi[vrednost - 1])
                {
                    return false;
                }
                brojevi[vrednost - 1] = true;
            }
        }
        return true;
    }

    public bool ProveriKrajIgre()
    {
        for (int i = 0; i < 9; i++)
        {
            if (!ProveriRed(i) || !ProveriKolonu(i))
            {
                return false;
            }
        }

        for (int red = 0; red < 9; red += 3)
        {
            for (int kolona = 0; kolona < 9; kolona += 3)
            {
                if (!ProveriKvadrant(red, kolona))
                {
                    return false;
                }
            }
        }

        return true;

    }
}