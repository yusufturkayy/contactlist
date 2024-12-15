using MyContacts.Model;

namespace MyContacts.Views;

[QueryProperty(nameof(id), "id")]
public partial class EditContactPage : ContentPage
{
    ContactsRepository repository = new ContactsRepository();
    private ContactInfo currentContact;
    public string id { get; set; }

    public EditContactPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        currentContact = repository.GetContact(Int32.Parse(id));
        LoadContactData();
    }

    private void LoadContactData()
    {
        NameEntry.Text = currentContact.NameSurname;
        PhoneEntry.Text = currentContact.PhoneNumber;
        EmailEntry.Text = currentContact.Email;
    }

    private async void UpdateButton_Clicked(object sender, EventArgs e)
    {
        currentContact.NameSurname = NameEntry.Text;
        currentContact.PhoneNumber = PhoneEntry.Text;
        currentContact.Email = EmailEntry.Text;

        repository.UpdateContact(currentContact);
        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirm Delete", 
            "Are you sure you want to delete this contact?", "Yes", "No");
            
        if (answer)
        {
            repository.DeleteContact(currentContact.Id);
            await Shell.Current.GoToAsync("..");
        }
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}