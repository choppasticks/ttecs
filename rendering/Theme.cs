public static class Theme
{
    public static string Reset = "\x1b[0m";
    
    public static string White = "\x1b[38;2;248;248;242m";
    public static string Selection = "\x1b[48;2;56;60;74m\x1b[38;2;129;161;193m";
    public static string FolderColor = "\x1b[38;2;139;233;253m";

    public static string Rgb(int r, int g, int b) => $"\x1b[38;2;{r};{g};{b}m";
}