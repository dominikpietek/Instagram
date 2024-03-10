using Instagram.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class DeleteStoryCommand : CommandBase
    {
        private readonly IStoryRepository _storyRepository;
        private readonly int _storyId;

        public DeleteStoryCommand(IStoryRepository storyRepository, int storyId)
        {
            _storyRepository = storyRepository;
            _storyId = storyId;
        }

        public async override void Execute(object parameter)
        {
            await _storyRepository.RemoveStoryAsync(_storyId);
            // update stories
        }
    }
}
