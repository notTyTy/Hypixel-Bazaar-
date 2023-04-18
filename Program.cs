using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;


class Program
{
    static async Task Main()
    {
        HttpClient httpClient = new HttpClient();
        string apiUrl = "https://api.hypixel.net/skyblock/bazaar";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {

                string content = await response.Content.ReadAsStringAsync();
                    JsonSerializer.Deserialize<bazaar_response>(content);
                    bazaar_response data = JsonSerializer.Deserialize<bazaar_response>(content);
                    bazaar_return(data);
            }
            else
            {
                Console.WriteLine("API Request failed with status code: " + response.StatusCode);
            }
    }
    static string user_return()
    {
        Console.Write("Please input a product type: "); // User Input
        string user_input = Console.ReadLine().ToUpper().Replace(" ", "_"); // converts user input to readable JSon format
        if (user_input == "POTATO" || user_input == "CARROT")
        { 
            user_input += "_ITEM";
        }
        return user_input;
    }
    static void bazaar_return(bazaar_response data)
    {               

        string user_input = user_return();
        Console.WriteLine(JsonSerializer.Serialize("Current Sell Price: " + data.products[user_input].quick_status.sellPrice));
        Console.WriteLine(JsonSerializer.Serialize("Current Sell Volume: " + data.products[user_input].quick_status.sellVolume));
        Console.WriteLine(JsonSerializer.Serialize("Current Sell Orders: " + data.products[user_input].quick_status.sellOrders));
        Console.WriteLine(JsonSerializer.Serialize("Current Buy Price: " + data.products[user_input].quick_status.buyPrice));
        Console.WriteLine(JsonSerializer.Serialize("Current Buy Orders: " + data.products[user_input].quick_status.buyOrders));
    }
}

public class bazaar_response{
    public bool success {get; set;}
    public long lastUpdated {get; set;}
    public Dictionary<string, product> products {get; set;}
}
public class product
{
    public string product_id {get; set;}
    public product_status quick_status {get; set;}
    public product_sell_summary[] sell_summary {get; set;}
    public product_buy_summary[] buy_summary {get; set;}
}
public class product_status
{
    public string productId {get; set;}
    public float sellPrice {get; set;}
    public int sellVolume {get; set;}
    public int sellMovingWeek {get; set;}
    public int sellOrders {get; set;}
    public float buyPrice {get; set;}
    public int buyVolume {get; set;}
    public int buyOrders {get; set;}
}

public class product_sell_summary 
{
    public int amount {get; set;}
    public float pricePerUnit {get; set;}
    public int orders {get; set;}
}
public class product_buy_summary
{
    public int amount {get; set;}
    public float pricePerUnit {get; set;}
    public int orders {get; set;}
}
