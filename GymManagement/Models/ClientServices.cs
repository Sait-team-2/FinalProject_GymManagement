using System;
using System.Collections.Generic;
using System.Linq;
using GymManagement.Models;

namespace GymManagement.Services
{
    public class ClientService
    {
        private List<Client> clients = new List<Client>();

        public List<Client> GetAllClients()
        {
            return clients;
        }

        public void AddClient(Client client)
        {
            clients.Add(client);
        }

        public void RemoveClient(int clientId)
        {
            clients.RemoveAll(c => c.Id == clientId);
        }

        public void SaveClient(Client client)
        {
            var existingClient = clients.FirstOrDefault(c => c.Id == client.Id);
            if (existingClient != null)
            {
                // Update existing client
                existingClient.FirstName = client.FirstName;
                existingClient.LastName = client.LastName;
                existingClient.Email = client.Email;
                existingClient.ContactNumber = client.ContactNumber;
                existingClient.Notes = client.Notes;
                existingClient.DateOfBirth = client.DateOfBirth;
            }
            else
            {
                // Add new client
                client.Id = clients.Count > 0 ? clients.Max(c => c.Id) + 1 : 1;
                clients.Add(client);
            }
        }

        public void DeleteClient(int clientId)
        {
            clients.RemoveAll(c => c.Id == clientId);
        }

        public List<Client> SearchClients(string searchText)
        {
            return clients.Where(c => c.FirstName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                      c.LastName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                      c.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public bool IsValidClient(Client client)
        {
            // Basic validation logic
            return !string.IsNullOrEmpty(client.FirstName) &&
                   !string.IsNullOrEmpty(client.LastName) &&
                   !string.IsNullOrEmpty(client.Email) &&
                   !string.IsNullOrEmpty(client.ContactNumber) &&
                   IsValidEmail(client.Email) &&
                   IsValidContactNumber(client.ContactNumber);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidContactNumber(string contactNumber)
        {
            return contactNumber.All(char.IsDigit);
        }
    }
}
